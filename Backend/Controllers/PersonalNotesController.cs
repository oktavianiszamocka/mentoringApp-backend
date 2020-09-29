using System.Linq;
using System.Threading.Tasks;
using MentorApp.Extensions;
using MentorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalNotesController : ControllerBase
    {
        private const int DefaultPageSize = 10;
        private readonly s17874Context _context;

        public PersonalNotesController(s17874Context context)
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