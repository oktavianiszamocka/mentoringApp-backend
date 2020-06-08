using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorApp.Controllers
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
        public async Task<IActionResult> GetUserProject(int idUser)
        {
            return Ok(await _context.ProjectMembers
                                        .Where(project => project.Member.Equals(idUser))
                                        .Select(project => project.Project)
                                        .ToListAsync());
        }

    }
}