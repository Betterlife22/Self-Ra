

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.MentorModel;
using Selfra_ModelViews.Model.PostModel;
using Selfra_ModelViews.Model.UserModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;

namespace Selfra_Services.Service
{
    public class MentorService : IMentorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public MentorService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task CreateMentor(CreateMentorModel model)
        {
            ApplicationUser user = await _unitOfWork.GetRepository<ApplicationUser>().Entities.FirstOrDefaultAsync(u => u.Id == model.UserId && !u.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy UserID");
            Mentor? check = await _unitOfWork.GetRepository<Mentor>().Entities.FirstOrDefaultAsync(m => m.UserId == model.UserId && !m.DeletedTime.HasValue);
                if(check != null) throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "User nay da co Mentor");
            Mentor mentor = _mapper.Map<Mentor>(model);

            user.isMentor = true;
            
            mentor.CreatedTime = DateTime.Now;
            mentor.CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
          

            await _unitOfWork.GetRepository<Mentor>().AddAsync(mentor);
            user.UserMentorId = mentor.Id;
            await _unitOfWork.GetRepository<ApplicationUser>().UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteMentor(string mentorId)
        {
            Mentor mentor = await _unitOfWork.GetRepository<Mentor>().Entities.FirstOrDefaultAsync(m => m.Id == mentorId && !m.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy mentor");

            mentor.DeletedTime = DateTime.Now;
            mentor.DeletedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<Mentor>().UpdateAsync(mentor);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedList<ResponseMentorModel>> GetAllMentor(string? searchName, int index, int PageSize)
        {
            IQueryable<ResponseMentorModel> query = from mentor in _unitOfWork.GetRepository<Mentor>().Entities.Include(m => m.User)
                                                    where !mentor.DeletedTime.HasValue
                                                  select new ResponseMentorModel
                                                  {
                                                      Id = mentor.Id,
                                                      UserId = mentor.UserId,
                                                      Bio = mentor.Bio,
                                                      ExpertiseAreas = mentor.ExpertiseAreas,
                                                      IsAvailable = mentor.IsAvailable,
                                                      Rating = mentor.Rating,
                                                      TotalReviews = mentor.TotalReviews,
                                                      User = new ResponseUserModel
                                                      {
                                                          Id = mentor.User.Id.ToString(),
                                                          Username = mentor.User.UserName,
                                                          Email = mentor.User.Email,
                                                          isMentor = mentor.User.isMentor,
                                                          MentorId = mentor.User.UserMentorId,
                                                          UserPackageName = mentor.User.UserPackageName,
                                                          PackageId = mentor.User.PackageId,
                                                          FullName = mentor.User.FullName,
                                                          PhoneNumber = mentor.User.PhoneNumber,
                                                          Role = "Mentor",
                                                          CreatedTime = mentor.User.CreatedTime
                                                      }
                                                  };

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(s => s.UserId.ToString()!.Contains(searchName));
            }

            PaginatedList<ResponseMentorModel> paginatedMentor = await _unitOfWork.GetRepository<ResponseMentorModel>().GetPagingAsync(query, index, PageSize);
            return paginatedMentor;
        }

        public async Task<ResponseMentorModel> GetMentorById(string userId)
        {

            Mentor mentor = await _unitOfWork.GetRepository<Mentor>().Entities.FirstOrDefaultAsync(m => m.UserId.ToString() == userId && !m.DeletedTime.HasValue)
               ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy mentor");

            ResponseMentorModel model = _mapper.Map<ResponseMentorModel>(mentor);
            model.User.Role = "Mentor";

            return model;

        }

        public async Task UpdateMentor(UpdateMentorModel model)
        {
            Mentor mentor = await _unitOfWork.GetRepository<Mentor>().Entities.FirstOrDefaultAsync(m => m.Id == model.MentorId && !m.DeletedTime.HasValue)
               ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy mentor");

            _mapper.Map(model, mentor);

            mentor.LastUpdatedTime = DateTime.Now;
            mentor.LastUpdatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<Mentor>().UpdateAsync(mentor);
            await _unitOfWork.SaveAsync();
        }
    }
}
