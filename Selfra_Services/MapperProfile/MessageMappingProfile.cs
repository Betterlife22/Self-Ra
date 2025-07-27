using AutoMapper;
using Selfra_Entity.Entity;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.MessageModel;
using Selfra_ModelViews.Model.ZaloModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.MapperProfile
{
    public class MessageMappingProfile :Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<Message, SendMessageModel>().ReverseMap();
            CreateMap<Message, MessageViewModel>().ForMember(dest => dest.SenderName,
               opt => opt.MapFrom(src => src.Sender != null ? src.Sender.UserName : "Unknown")).ReverseMap();
            CreateMap<Conversation, ConversationViewModel>()
                .AfterMap((src, dest, context) =>
                {
                    var currentUserId = (Guid)context.Items["CurrentUserId"];

                    if (!src.IsGroup)
                    {
                        var otherParticipant = src.Participants
                            .FirstOrDefault(p => p.UserId != currentUserId);

                        if (otherParticipant?.User != null)
                        {
                            dest.ConversationName = otherParticipant?.User?.UserName ?? "Unknown";
                        }
                    }

                    dest.Messages = src.Messages != null
                        ? src.Messages.Select(m => context.Mapper.Map<MessageViewModel>(m)).ToList()
                        : new List<MessageViewModel>();
                }); CreateMap<ZaloGroup,ZaloViewModel>().ReverseMap();
        }
    }
}
