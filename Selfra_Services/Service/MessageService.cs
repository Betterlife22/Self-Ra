using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.MessageModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.Service
{
    public class MessageService : IMessageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MessageService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddMembertoGroup(string userId, string conservationId)
        {
            var newparticipant = new ConversationParticipant()
            {
                ConversationId = conservationId,
                UserId =Guid.Parse(userId),
                IsAdmin = false,
                CreatedTime = DateTime.UtcNow

            };
            await _unitOfWork.GetRepository<ConversationParticipant>().AddAsync(newparticipant);
            await _unitOfWork.SaveAsync();
        }

        public async Task CreateGroupConversation(GroupModel groupmodel)
        {
            var groupconversation = new Conversation()
            {
                ConversationName = groupmodel.ConversationName,
                IsGroup = true
            };
            await _unitOfWork.GetRepository<Conversation>().AddAsync(groupconversation);
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var admingroup = new ConversationParticipant()
            {
                ConversationId = groupconversation.Id,
                UserId = Guid.Parse(userId),
                IsAdmin = true,
                CreatedTime = DateTime.UtcNow,

            };
            await _unitOfWork.GetRepository<ConversationParticipant>().AddAsync(admingroup);
            foreach (var member in groupmodel.GroupsMemberId)
            {
                var newparticipant = new ConversationParticipant()
                {
                    ConversationId = groupconversation.Id,
                    UserId = member,
                    IsAdmin = false,
                    CreatedTime = DateTime.UtcNow

                };
                await _unitOfWork.GetRepository<ConversationParticipant>().AddAsync(newparticipant);
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<ConversationViewModel>> GetAllUserConservation()
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var conversationList = await _unitOfWork.GetRepository<Conversation>()
                .GetAllByPropertyAsync(c=>c.Participants.Any(u=>u.UserId == Guid.Parse(userId)),includeProperties:"Participants");
            var result = _mapper.Map<List<ConversationViewModel>>(conversationList, opt =>
            {
                opt.Items["CurrentUserId"] = Guid.Parse(userId);
            });
            return result;
        }

        public async Task<ConversationViewModel> GetConversation(string id)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var conversation = await _unitOfWork.GetRepository<Conversation>().GetByPropertyAsync(c=>c.Id == id,includeProperties:"Messages");
            var result = _mapper.Map<ConversationViewModel>(conversation, opt =>
            {
                opt.Items["CurrentUserId"] = Guid.Parse(userId);
            });
            return result;
        }

        public async Task<List<MessageViewModel>> GetMessages(string conversationid)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var messages = await _unitOfWork.GetRepository<Message>().GetAllByPropertyAsync(m => m.ConversationId == conversationid);
            var result = _mapper.Map<List<MessageViewModel>>(messages);
            return result;
        }

        public async Task MarkMessageAsRead(string conversationId)
        {
            var userid = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var unreadMessage = await _unitOfWork.GetRepository<Message>().GetAllByPropertyAsync(
                m => m.ConversationId == conversationId && m.SenderId == Guid.Parse(userid) && !m.IsRead);
            foreach (var item in unreadMessage)
            {
                item.IsRead = true;
                
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task SendMessage(SendMessageModel messagemodel)
        {
            var senderId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var message = _mapper.Map<Message>(messagemodel);
            message.SenderId = Guid.Parse(senderId);            
            await _unitOfWork.GetRepository<Message>().AddAsync(message);
            var conversation = await _unitOfWork.GetRepository<Conversation>().GetByIdAsync(messagemodel.ConversationId);          
            var sender = await _unitOfWork.GetRepository<ApplicationUser>().GetByPropertyAsync(s=>s.Id == Guid.Parse(senderId));
            conversation.LastMessage = messagemodel.Content;
            conversation.LastSenderName = sender.UserName;
            await _unitOfWork.SaveAsync();
        }

        public async Task StartConversation(string secondUserid)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var secondUser = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(Guid.Parse(secondUserid));

            var conversation = new Conversation()
            {
                ConversationName = secondUser.FullName,
                IsGroup = false
            };
            await _unitOfWork.GetRepository<Conversation>().AddAsync(conversation);

            var firstParticipant = new ConversationParticipant()
            {
                ConversationId = conversation.Id,
                UserId = Guid.Parse(userId),
                CreatedTime = DateTime.UtcNow,
                IsAdmin = false
            };
            await _unitOfWork.GetRepository<ConversationParticipant>().AddAsync(firstParticipant);
            var secondParticipant = new ConversationParticipant()
            {
                ConversationId = conversation.Id,
                UserId = Guid.Parse(secondUserid),
                CreatedTime = DateTime.UtcNow,
                IsAdmin = false
            };
            await _unitOfWork.GetRepository<ConversationParticipant>().AddAsync(secondParticipant);
            await _unitOfWork.SaveAsync();

        }
    }
}
