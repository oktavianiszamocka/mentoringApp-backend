using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Tests
{
    public class S3BacketTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}