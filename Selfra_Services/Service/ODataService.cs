using AutoMapper;
using AutoMapper.QueryableExtensions;
using Google.Api;
using Microsoft.EntityFrameworkCore;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CourseModel;
using Selfra_ModelViews.Model.LessonModel;
using Selfra_ModelViews.Model.MentorContact;
using Selfra_ModelViews.Model.MentorModel;
using Selfra_ModelViews.Model.PackageModel;
using Selfra_ModelViews.Model.UserModel;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.Service
{
    public class ODataService : IODataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ODataService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IQueryable<CourseViewModel> GetCourses()
        {
            return _unitOfWork.GetRepository<Course>()
             .GetByOdata()
             .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider);
        }

        public IQueryable<LessonViewModel> GetLessons()
        {
            return _unitOfWork.GetRepository<Lesson>()
             .GetByOdata()
             .ProjectTo<LessonViewModel>(_mapper.ConfigurationProvider);
        }

        public IQueryable<ResponseMentorContact> GetMentorContacts()
        {
            return _unitOfWork.GetRepository<MentorContact>()
                        .GetByOdata()
                        .ProjectTo<ResponseMentorContact>(_mapper.ConfigurationProvider);
        }

        public IQueryable<ResponseMentorModel> GetMentors()
        {
            return _unitOfWork.GetRepository<Mentor>()
                        .GetByOdata()
                        .ProjectTo<ResponseMentorModel>(_mapper.ConfigurationProvider);
        }

        public IQueryable<ResponsePackageModel> GetPackages()
        {
            return _unitOfWork.GetRepository<Package>()
            .GetByOdata()
            .ProjectTo<ResponsePackageModel>(_mapper.ConfigurationProvider);
        }

        public IQueryable<ResponseUserModel> GetUsers()
        {
            var users = _unitOfWork.GetRepository<ApplicationUser>().GetByOdata();
            var roles = _unitOfWork.GetRepository<ApplicationRole>().GetByOdata();
            var userRoles = _unitOfWork.GetRepository<ApplicationUserRoles>().GetByOdata();
            var packages = _unitOfWork.GetRepository<Package>().GetByOdata();

            var query = from u in users
                        join ur in userRoles on u.Id equals ur.UserId
                        join r in roles on ur.RoleId equals r.Id
                        join p in packages on u.PackageId equals p.Id into up
                        from p in up.DefaultIfEmpty()
                        select new ResponseUserModel
                        {
                            Id = u.Id.ToString(),
                            UserName = u.UserName,
                            Email = u.Email,
                            FullName = u.FullName,
                            isMentor = u.isMentor ?? false,
                            MentorId = u.UserMentorId,
                            PhoneNumber = u.PhoneNumber,
                            CreatedTime = u.CreatedTime,
                            Role = r.Name,
                            PackageId = p != null ? p.Id : "No Package ID",
                            UserPackageName = p != null ? p.Name : "No Package"
                        };

            return query;
        }

    }
}
