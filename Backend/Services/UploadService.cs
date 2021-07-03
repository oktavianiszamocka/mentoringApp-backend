using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using MentorApp.Helpers;
using MentorApp.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class UploadService : IUploadService
    {
        private readonly IAmazonS3 _client;
        public UploadService(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<S3Response> CreateBucketAsync(string bucketName)
        {
            try
            {
                if(await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName) == false)
                {
                    var putBacketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    var response = await _client.PutBucketAsync(putBacketRequest);
                    return new S3Response
                    {
                        Message = response.ResponseMetadata.RequestId,
                        Status = response.HttpStatusCode
                    };
                }
            } catch(AmazonS3Exception e)
            {
                return new S3Response
                {
                    Message = e.Message,
                    Status = e.StatusCode
                };
            } catch (Exception e)
            {
                return new S3Response
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError
                };
            }

            return new S3Response
            {
                Message = "Something went wrong",
                Status = HttpStatusCode.InternalServerError
            };
        }

        public async Task UploadUserAvatar()
        {
            const string filePath = "C:\\Users\\Tami\\Desktop\\mentoringApp-backend\\testAvatar.jpg";
            const string bucketName = "mentoring-app-avatars";
            const string fileStreamUpload = "FileStreamUpload";


            try
            {
                var fileTransferUtility = new TransferUtility(_client);

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
