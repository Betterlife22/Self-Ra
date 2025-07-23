

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.PaymentModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;
using System.Security.Cryptography;
using System.Text;
using Transaction = Selfra_Entity.Model.Transaction;

namespace Selfra_Services.Service
{
    public class PaymentService : IPayMentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PayOS _payOS;
        private readonly PayOSOptions _payOSOptions;
        private readonly ILogger _logger;
        public PaymentService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, PayOS payOS, IOptions<PayOSOptions> payosOptions, ILogger logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _payOS = payOS;
            _payOSOptions = payosOptions.Value;
            _logger = logger;
        }
        public async Task<CreatePaymentResultModel> CreatePaymentLinkAsync(string packageId)
        {
            Package package = await _unitOfWork.GetRepository<Package>().Entities.FirstOrDefaultAsync(p => p.Id == packageId && !p.DeletedTime.HasValue) 
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Package");

            var orderId = Guid.NewGuid();
            long rawCode = BitConverter.ToInt64(orderId.ToByteArray(), 0);
            rawCode = Math.Abs(rawCode);

            // Ensure it's at most 15 digits by applying modulo
            long orderCode = rawCode % 1_000_000_000_000_000;

            var item = new ItemData(package.Name, 1, (int)package.Price);

            var payMentData = new PaymentData(
                orderCode,
                (int)package.Price,
                $"Thanh toán gói {package.Name}",
                new List<ItemData> { item },
                $"{"myapp://payment-cancel"}/payment-cancel",
                $"{"myapp://payment-success"}/payment-success")
                ;
                
            var result =  await _payOS.createPaymentLink(payMentData);

            Transaction transaction = new Transaction
            {
                OrderId = orderId.ToString(),               
                PaymentLinkId = result.paymentLinkId,
                OrderCode = orderCode,
                PackageId = packageId,
                PaymentMethod = "PayOS",
                PaymentStatus = "Pending",
                Total = package.Price,
                CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor)
            };
            await _unitOfWork.GetRepository<Transaction>().AddAsync(transaction);
            await _unitOfWork.SaveAsync();

            return new CreatePaymentResultModel
            { 
                CheckoutUrl = result.checkoutUrl,
                OrderId = orderId.ToString()
            };
            
        }

        public async Task HandlePayOSWebhookAsync(string rawBody)
        {

            PayOSWebhookPayload payload =
                JsonConvert.DeserializeObject<PayOSWebhookPayload>(rawBody)
                ?? throw new Exception("Payload rỗng hoặc không hợp lệ.");

            long orderCode = payload.Data?.OrderCode
                ?? throw new Exception("Payload thiếu OrderCode.");

            Transaction transaction =
                _unitOfWork.GetRepository<Transaction>().Entities
                    .FirstOrDefault(t => t.OrderCode == orderCode)
                ?? throw new Exception("Không tìm thấy Transaction.");

            bool succeeded =
                payload.Success ||
                string.Equals(payload.Data?.Code, "00", StringComparison.OrdinalIgnoreCase);

            if (succeeded)
            {
                transaction.PaymentStatus = "Success";
                transaction.LastUpdatedTime = DateTime.UtcNow;

                

                UserPackage userPackage = new UserPackage
                {
                    PackageId = transaction.PackageId,
                    UserId = transaction.CreatedBy,
                    CreatedBy = transaction.CreatedBy
                };

                await _unitOfWork.GetRepository<UserPackage>().AddAsync(userPackage);
                transaction.UserPackageId = userPackage.Id;
            }
            else
            {
                transaction.PaymentStatus = "Failed";
            }

            await _unitOfWork.GetRepository<Transaction>().UpdateAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public bool IsValidData(string transaction, string transactionSignature)
        {
            try
            {
                JObject jsonObject = JObject.Parse(transaction);

                var sortedKeys = jsonObject.Properties()
                                           .Select(p => p.Name)
                                           .OrderBy(k => k, StringComparer.Ordinal)
                                           .ToList();

                var sb = new StringBuilder();
                for (int i = 0; i < sortedKeys.Count; i++)
                {
                    var key = sortedKeys[i];
                    var value = jsonObject[key]?.ToString();
                    sb.Append($"{key}={value}");
                    if (i < sortedKeys.Count - 1)
                        sb.Append("&");
                }

                string computedSignature = ComputeHmacSHA256(sb.ToString(), _payOSOptions.ChecksumKey);
                return computedSignature.Equals(transactionSignature, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        private string ComputeHmacSHA256(string message, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hash = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }


    
}
