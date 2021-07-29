using MentorApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IUploadService
    {
        Task UploadUserAvatar();
        Task<S3Response> CreateBucketAsync(string bucketName);
    }
}
