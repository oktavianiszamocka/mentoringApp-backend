using MentorApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly MentorAppContext _context;

        public ProjectsController(MentorAppContext context)
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