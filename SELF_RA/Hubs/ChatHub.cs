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
        public async Task SendMessage(SendMessageModel sendMessage, string token, string senderid)
        {
            var json = JsonSerializer.Serialize(sendMessage);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Set the bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Make the POST request
            var response = await _httpClient.PostAsync("https://localhost:7126/api/message/sendmessage", content);
            await Clients.All.SendAsync("ReceiveMessage",senderid, sendMessage.Content);

        }
        public async Task MarkAsRead (string conversationid,string token,string senderid)
        {
            var json = JsonSerializer.Serialize(conversationid);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Set the bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Make the POST request
            var response = await _httpClient.PostAsync("https://localhost:7126/api/message/sendmessage", content);
            await Clients.All.SendAsync("ReceiveNoti", conversationid,senderid);

        }
        public string GetConnectionId() { return Context.ConnectionId; }

    }
}
