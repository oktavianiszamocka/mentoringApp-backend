
using MentorApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Services;
using MentorApp.Helpers;
using MentorApp.Filter;
using MentorApp.Models;

namespace MentorApp.Controllers
{
    [Route("api/personal-notes")]
    [ApiController]
    public class PersonalNotesController : ControllerBase
    {
        private const int DefaultPageSize = 3;
        private readonly MentorAppContext _context;
        private readonly IUriService _uriService;

        public PersonalNotesController(MentorAppContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet("{idUser:int}")]
        public async Task<IActionResult> GetPersonalNoteList(int idUser, [FromQuery] Filter.PaginationFilter filter )
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var personalNoteList = await _context.PersonalNote
                          .Where(note => note.User.Equals(idUser))
                         .OrderByDescending(Note => Note.LastModified)
                         .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                         .Take(validFilter.PageSize)
                         .ToListAsync();

            var totalRecords = personalNoteList.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse<PersonalNote>(personalNoteList, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
    }
}