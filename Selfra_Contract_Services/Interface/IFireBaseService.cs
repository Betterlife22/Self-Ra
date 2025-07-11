using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Contract_Services.Interface
{
    public interface IFireBaseService
    {
        Task SaveOrUpdateTokenAsync(string username, string token);
        Task<string?> GetTokenByUserIdAsync(string userId);
        Task SendNotificationAsync(string token, string title, string body);
        Task<string> GetAccessTokenAsync();
    }
}
