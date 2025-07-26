

using Selfra_Core.Base;
using Selfra_ModelViews.Model.MentorModel;
using Selfra_ModelViews.Model.RoleModel;

namespace Selfra_Contract_Services.Interface
{
    public interface IMentorService
    {
        Task<PaginatedList<ResponseMentorModel>> GetAllMentor(string? searchName, int index, int PageSize);

        Task CreateMentor(CreateMentorModel model);

        Task UpdateMentor(UpdateMentorModel model);

        Task DeleteMentor(string mentorId);

        Task<ResponseMentorModel> GetMentorById(string mentorId);
    }
}
