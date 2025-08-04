using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.MessageModel;
using Selfra_Services.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Collections.Concurrent;
using Selfra_Core.Utils;

namespace SELF_RA.Hubs
{
    //[Authorize]
    public class ChatHub :Hub
    {
        private readonly IMessageService _messageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        public ChatHub(IMessageService messageService, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _messageService = messageService;   
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }
        public async Task SendMessage(string sendMessage, string token, string senderid)
        {
            //var json = JsonSerializer.Serialize(sendMessage);
            var content = new StringContent(sendMessage, Encoding.UTF8, "application/json");

            // Set the bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var message = JsonSerializer.Deserialize<SendMessageModel>(sendMessage,options);

            // Make the POST request
            var response = await _httpClient.PostAsync("https://selfra.azurewebsites.net/api/message/sendmessage", content);
            await Clients.All.SendAsync("ReceiveMessage",senderid, message.Content);

        }
        //public async Task MarkAsRead (string conversationid,string token,string senderid)
        //{
        //    var json = JsonSerializer.Serialize(conversationid);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    // Set the bearer token
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    // Make the POST request
        //    var response = await _httpClient.PostAsync("https://localhost:7126/api/message/sendmessage", content);
        //    await Clients.All.SendAsync("ReceiveNoti", conversationid,senderid);

        //}

        public override Task OnConnectedAsync()
        {
            var mentorId = Context.GetHttpContext()?.Request?.Query["mentorId"];
            if (!string.IsNullOrEmpty(mentorId))
            {
                MentorConnection.Add(mentorId, Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            MentorConnection.RemoveByConnectionId(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
