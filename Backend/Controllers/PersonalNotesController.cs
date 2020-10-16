using MentorApp.Extensions;
using MentorApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalNotesController : ControllerBase
    {
        private const int DefaultPageSize = 10;
        private readonly MentorAppContext _context;

        public PersonalNotesController(MentorAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonalNoteList(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            return Ok(await _context.PersonalNote
                         .OrderByDescending(Note => Note.LastModified)
                         .GetPage(pageNumber, pageSize)
                         .ToListAsync());
        }
    }
}