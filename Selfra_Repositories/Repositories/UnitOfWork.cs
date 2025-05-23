using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Selfra_Repositories.Base;
using Selft.Contract.Repositories.Interface;

namespace Selfra_Repositories.Repositories
{
    public class UnitOfWork(SelfraDBContext dbContext, IAmazonS3 s3Client, IConfiguration configuration) : IUnitOfWork
    {
        private bool disposed = false;
        private readonly SelfraDBContext _dbContext = dbContext;
        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucketName = configuration["AWS:S3BucketName"];


        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var key = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Upload the file to S3
            using (var stream = file.OpenReadStream())
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = file.ContentType,

                };
                putRequest.Metadata.Add("Content-Disposition", "inline");


                await _s3Client.PutObjectAsync(putRequest);
            }

            // Return the URL of the uploaded file
            return $"https://{_bucketName}.s3.amazonaws.com/{key}";
        }
    }
}
