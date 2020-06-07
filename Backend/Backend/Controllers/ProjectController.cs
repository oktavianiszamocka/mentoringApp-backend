using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly s17874Context _context;

        public ProjectController(s17874Context context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetUserProject(int IdUser)
        {
            var result = _context.ProjectMembers.
                                        Where(project => project.Member.Equals(IdUser))
                                        .Select(project => project.Project)
                                       .ToList();
            return Ok(result);
        }

    }
}