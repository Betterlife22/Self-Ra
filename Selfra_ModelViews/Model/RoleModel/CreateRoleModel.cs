

using Microsoft.AspNetCore.Http;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using System.Text.RegularExpressions;

namespace Selfra_ModelViews.Model.RoleModel
{
    public class CreateRoleModel
    {
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public void ValidateFields()
        {
            ValidateField(Name!, "Tên vai trò");
            ValidateField(FullName!, "FullName");
        }

        // Phương thức riêng để xác thực từng trường
        private void ValidateField(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, $"{fieldName} không thể để trống hoặc chỉ chứa khoảng trắng.");
            }
            value = value.Trim();

            // Kiểm tra ký tự đặc biệt
            if (value.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)))
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, $"{fieldName} không được chứa ký tự đặc biệt.");
            }
            else if (fieldName.Equals("FullName", StringComparison.OrdinalIgnoreCase))
            {
                if (value != value.Trim())
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, $"{fieldName} không được chứa khoảng trắng đầu hoặc cuối.");
                }
                if (Regex.IsMatch(value, @"\s{2,}"))
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, $"{fieldName} không được chứa nhiều khoảng trắng liên tiếp giữa các từ.");
                }
                if (value.Any(char.IsDigit))
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, $"{fieldName} không được chứa số.");
                }
            }
            else if (fieldName.Equals("Tên vai trò", StringComparison.OrdinalIgnoreCase))
            {
                if (value.Contains(" "))
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, $"{fieldName} không được chứa khoảng trắng.");
                }
            }
            else
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, $"Trường không xác định: {fieldName}");
            }
        }
    }
}
