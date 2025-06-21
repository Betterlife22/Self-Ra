using Selfra_Entity.Model;
using Selfra_ModelViews.Model.MessageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Contract_Services.Interface
{
    public interface IMessageService
    {
        Task StartConversation(string secondUserId);
        Task CreateGroupConversation (GroupModel groupmodel);
        Task SendMessage (SendMessageModel message);
        Task AddMembertoGroup(string userId, string conservationId);
        Task<ConversationViewModel> GetConversation (string id);
        Task<List<ConversationViewModel>> GetAllUserConservation();
        Task<List<MessageViewModel>> GetMessages (string conversationid);
        Task MarkMessageAsRead(string conversationId);
    }
}
