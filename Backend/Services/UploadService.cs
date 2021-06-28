using Amazon.S3;
using Amazon.S3.Transfer;
using MentorApp.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class UploadService : IUploadService
    {
        private readonly IUserRepository _userRepository;
        public UploadService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UploadUserAvatar(string imageUrl)
        {
            const string filePath = "C:\\Users\\Tami\\Desktop\\mentoringApp-backend\\testAvatar.jpg";
            const string bucketName = "mentoring-app-avatars";
            const string fileStreamUpload = "FileStreamUpload";

            try
            {
                var fileTransferUtility = new TransferUtility();

                using (var fileToUpload = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    await fileTransferUtility.UploadAsync(fileToUpload, bucketName, fileStreamUpload);
                }

            } catch (AmazonS3Exception e)
            {
                Console.WriteLine(e.Message);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
