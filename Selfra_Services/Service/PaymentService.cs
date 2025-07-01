

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using Net.payOS.Types;
using Newtonsoft.Json;
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
        

        public PaymentService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, PayOS payOS)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _payOS = payOS;
        }
        public async Task<CreatePaymentResultModel> CreatePaymentLinkAsync(string packageId)
        {
            Package package = await _unitOfWork.GetRepository<Package>().Entities.FirstOrDefaultAsync(p => p.Id == packageId && !p.DeletedTime.HasValue) 
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Package");

            string orderId = Guid.NewGuid().ToString("N");
            long orderCode = long.Parse(
                DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString().PadRight(15, '0')[..15]);

            var item = new ItemData(package.Name, 1, (int)package.Price);

            var payMentData = new PaymentData(
                orderCode,
                (int)package.Price,
                $"Thanh toán gói {package.Name}",
                new List<ItemData> { item },
                $"{"https://vi.wikipedia.org/wiki/Th%C3%A0nh_c%C3%B4ng"}/payment-success",
                $"{"https://vi.wikipedia.org/wiki/Th%E1%BA%A5t_b%E1%BA%A1i"}/payment-cancel");
                
            var result =  await _payOS.createPaymentLink(payMentData);

            Transaction transaction = new Transaction
            {
                OrderId = orderId,
                PaymentLinkId = result.paymentLinkId,
                PackageId = packageId,
                PaymentMethod = "PayOS",
                PaymentStatus = "Pending",
                Total = package.Price,
            };
            await _unitOfWork.GetRepository<Transaction>().AddAsync(transaction);
            await _unitOfWork.SaveAsync();

            return new CreatePaymentResultModel
            { 
                CheckoutUrl = result.checkoutUrl,
                OrderId = orderId
            };
            
        }

        public async Task HandlePayOSWebhookAsync(string rawBody, string checksumHeader)
        {
            if(VerifyWebhookSignature(rawBody, checksumHeader))
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "Signature không hợp lệ.");
            }
            var data = JsonConvert.DeserializeObject<PayOSWebhookPayload>(rawBody);

            var ordercode = data.orderCode.ToString();

            Transaction transaction = _unitOfWork.GetRepository<Transaction>().Entities.FirstOrDefault(t => t.OrderId == ordercode)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Transaction");

            if (data.status == "SUCCEEDED")
            {
                transaction.PaymentStatus = "Success";
                transaction.LastUpdatedTime = DateTime.Now;
                UserPackage userPackage = new UserPackage
                {
                    PackageId = transaction.PackageId,
                    UserId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor),
                    CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor),
                };

                await _unitOfWork.GetRepository<UserPackage>().AddAsync(userPackage);

            }
            else
            {
                transaction.PaymentStatus = "Failed";
            }

            
            await _unitOfWork.GetRepository<Transaction>().UpdateAsync(transaction);
            await _unitOfWork.SaveAsync();

        }

        private bool VerifyWebhookSignature(string rawBody, string checksumHeader)
        {
            var payos = new PayOSOptions();
            // Dùng checksum key từ cấu hình
            var checksumKey = payos.ChecksumKey;
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(checksumKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawBody));
            var calculated = BitConverter.ToString(hash).Replace("-", "").ToLower();

            return calculated == checksumHeader.ToLower();
        }
    }
}
