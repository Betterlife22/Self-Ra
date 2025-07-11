using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Entity;
using Selfra_Entity.Model;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.Service
{
    public class FireBaseServicen : IFireBaseService
    {
        private readonly FireBaseSettings _settings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FirestoreDb _firestoreDb;
        public FireBaseServicen(IOptions<FireBaseSettings> options, IUnitOfWork unitOfWork)
        {
            _settings = options.Value;
            _unitOfWork = unitOfWork;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", _settings.ServiceAccountPath);
            _firestoreDb = FirestoreDb.Create(_settings.ProjectId);
        }
        public async Task<string> GetAccessTokenAsync()
        {
            using var stream = new FileStream(_settings.ServiceAccountPath, FileMode.Open, FileAccess.Read);
            var credential = GoogleCredential.FromStream(stream)
                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

            return await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        }

        public async Task<string?> GetTokenByUserIdAsync(string userId)
        {
            var token = await _unitOfWork.GetRepository<FcmToken>().GetByPropertyAsync(t => t.UserId == Guid.Parse(userId));
            return token.Token;

        }

        public async Task SaveOrUpdateTokenAsync(string username, string token)
        {

            var user = await _unitOfWork.GetRepository<ApplicationUser>().GetByPropertyAsync(u => u.UserName == username);
            var existing = await _unitOfWork.GetRepository<FcmToken>().GetByPropertyAsync(t => t.UserId == user.Id);

            if (existing != null)
            {
                existing.Token = token;
                existing.LastUpdatedTime = DateTime.UtcNow;
            }
            else
            {
                var newtoken = new FcmToken()
                {
                    UserId = user.Id,
                    Token = token,
                    CreatedTime = DateTime.UtcNow
                };
                await _unitOfWork.GetRepository<FcmToken>().AddAsync(newtoken);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task SendNotificationAsync(string token, string title, string body)
        {
            var accessToken = await GetAccessTokenAsync();

            var message = new
            {
                message = new
                {
                    token = token,
                    notification = new
                    {
                        title = title,
                        body = body
                    }
                }
            };

            var json = JsonConvert.SerializeObject(message);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.PostAsync(
                $"https://fcm.googleapis.com/v1/projects/{_settings.ProjectId}/messages:send",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new Exception($"Push failed: {result}");
            }
            await SaveNotificationToFirestore(title, body);

        }
        private async Task SaveNotificationToFirestore(string title, string body)
        {
                var doc = new Dictionary<string, object>
            {
                { "title", title },
                { "body", body },
                { "time", GetRelativeTime() }, // e.g. "Just now"
                { "createdAt", Timestamp.GetCurrentTimestamp() }
            };

            await _firestoreDb.Collection("notifications").AddAsync(doc);
        }

        private string GetRelativeTime()
        {
            return "Just now"; // You can customize this with your own logic
        }
    }
}
