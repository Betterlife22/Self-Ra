using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.MessageModel;
using Selfra_Services.Infrastructure;

namespace SELF_RA.Hubs
{
    [Authorize]
    public class ChatHub :Hub
    {
        private readonly IMessageService _messageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChatHub(IMessageService messageService, IHttpContextAccessor httpContextAccessor)
        {
            _messageService = messageService;   
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SendMessage(SendMessageModel sendMessage)
        {
            var senderId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

            await _messageService.SendMessage(sendMessage);
            await Clients.All.SendAsync("ReceiveMessage", senderId, sendMessage.Content);

        }
        public async Task MarkAsRead (string conversationid)
        {
            await _messageService.MarkMessageAsRead(conversationid);
            await Clients.All.SendAsync("ReceiveNoti", conversationid);

        }
    }
}
