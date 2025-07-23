

using Selfra_Core.Base;
using Selfra_ModelViews.Model.MentorContact;

namespace Selfra_Contract_Services.Interface
{
    public interface IMentorContactService
    {
        Task<PaginatedList<ResponseMentorContact>> GetAllMentorContact(string? userId,string? MentorId, int index, int PageSize);

        Task<ResponseMentorContact> GetMentorContactById (string? id);  

         Task CreateMentorContact (CreateMentorContact createMentorContact);

        Task UpdateMentorContact (UpdateMentorContact updateMentorContact);


        Task DeleteMentorContact (string? id);
        //Task NotifyMentorAsync(string mentorId, string message);
    }
}
