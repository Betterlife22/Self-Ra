
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using Selfra_Core.Utils;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.MentorContact;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;

namespace Selfra_Services.Service
{
    public class MentorContactService : IMentorContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly IHubContext<Hub> _hubContext;

        public MentorContactService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

        public async Task CreateMentorContact(CreateMentorContact createMentorContact)
        {
            Mentor check = await _unitOfWork.GetRepository<Mentor>().Entities.FirstOrDefaultAsync(c => c.Id == createMentorContact.MentorId && !c.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Mentor");

            ApplicationUser checkUser = await _unitOfWork.GetRepository<ApplicationUser>().Entities.FirstOrDefaultAsync(c => c.Id == createMentorContact.UserId && !c.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy User");

            MentorContact mentorContact = _mapper.Map<MentorContact>(createMentorContact);
            mentorContact.CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            mentorContact.CreatedTime = DateTime.Now;
            await _unitOfWork.GetRepository<MentorContact>().AddAsync(mentorContact);
            await _unitOfWork.SaveAsync();
            }

        public async Task DeleteMentorContact(string? id)
        {
            MentorContact check = await _unitOfWork.GetRepository<MentorContact>().Entities.FirstOrDefaultAsync(c => c.Id == id && !c.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thây MentorContact");

            check.DeletedTime = DateTime.Now;
            check.DeletedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<MentorContact>().UpdateAsync(check);
            await _unitOfWork.SaveAsync();

        }

        public async Task<PaginatedList<ResponseMentorContact>> GetAllMentorContact(string? MentorId, int index, int PageSize)
        {
            IQueryable<ResponseMentorContact> query = from mentorcontact in _unitOfWork.GetRepository<MentorContact>().Entities
                                                      where !mentorcontact.DeletedTime.HasValue
                                                      select new ResponseMentorContact
                                                      {
                                                          MentorContactId = mentorcontact.Id,
                                                          MentorId = mentorcontact.MentorId,
                                                          Message = mentorcontact.Message,
                                                          Status = mentorcontact.Status,
                                                          UserId = mentorcontact.UserId
                                                      };

            if (!string.IsNullOrWhiteSpace(MentorId))
            {
                query = query.Where(s => s.MentorId.ToString()!.Contains(MentorId));
            }

            PaginatedList<ResponseMentorContact> paginatedMentorContact = await _unitOfWork.GetRepository<ResponseMentorContact>().GetPagingAsync(query, index, PageSize);
            return paginatedMentorContact;
        }

        public async Task<ResponseMentorContact> GetMentorContactById(string? id)
        {
            MentorContact mentorContact = await _unitOfWork.GetRepository<MentorContact>().Entities.FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tim thấy mentorcontact");

            ResponseMentorContact model = _mapper.Map<ResponseMentorContact>(mentorContact);

            return model;
        }

        //public async Task NotifyMentorAsync(string mentorId, string message)
        //{
        //    var connectionId = MentorConnection.GetConnectionId(mentorId);
        //    if (!string.IsNullOrEmpty(connectionId))
        //    {
        //        await _hubContext.Clients.Client(connectionId)
        //            .SendAsync("ReceiveNotification", message);
        //    }
        //}

        public async Task UpdateMentorContact(UpdateMentorContact updateMentorContact)
        {
            MentorContact mentorContact = await _unitOfWork.GetRepository<MentorContact>().Entities.FirstOrDefaultAsync(m => m.Id == updateMentorContact.MentorContactId && !m.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tim thấy mentorcontact");

            _mapper.Map(updateMentorContact, mentorContact);

            mentorContact.LastUpdatedTime = DateTime.Now;
            mentorContact.LastUpdatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<MentorContact>().UpdateAsync(mentorContact);
            await _unitOfWork.SaveAsync();
        }
    }
}
