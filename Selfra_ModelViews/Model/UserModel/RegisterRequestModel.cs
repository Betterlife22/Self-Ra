
using Microsoft.AspNetCore.Http;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using System.Text.RegularExpressions;

namespace Selfra_ModelViews.Model.UserModel
{
    public class RegisterRequestModel
    {
        public required string UserName { get; set; }

        public string? Email { get; set; }
        public required string Password { get; set; }
        public required string RoleId { get; set; }
        public string? FullName { get; set; }

        public void CheckValid()
        {
            if (string.IsNullOrWhiteSpace(UserName) || !Regex.IsMatch(UserName, @"^[a-zA-Z0-9]+$"))
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Username chỉ được chứa chữ cái và số.");
            }
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6 ||
                !Regex.IsMatch(Password, @"[A-Z]") || !Regex.IsMatch(Password, @"\W"))
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Password phải có tối thiểu 6 kí tự, 1 kí tự viết hoa, 1 kí tự đặc biệt.");
            }
            if (!string.IsNullOrWhiteSpace(FullName) && !Regex.IsMatch(FullName, @"^[a-zA-ZÀ-ỹ\s]+$"))
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Fullname không được chứa kí tự đặc biệt và số.");
            }
        }
    }
}
