using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Tests
{
    [Route("api/S3Bucket")]
    public class S3BucketTestController : Controller
    {
        private readonly IUploadService _uploadService;
        public S3BucketTestController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            await _uploadService.UploadUserAvatar();
            return Ok();
        }
    }
}