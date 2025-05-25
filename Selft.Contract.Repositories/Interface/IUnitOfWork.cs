

using Microsoft.AspNetCore.Http;

namespace Selft.Contract.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        void Save();
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();
        Task<string> UploadFileAsync(IFormFile file);
    }
}
