using System.Linq;
using System.Threading.Tasks;
using MentorApp.Filter;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Controllers
{
    [Route("api/personal-notes")]
    [ApiController]
    public class PersonalNotesController : ControllerBase
    {
        private const int DefaultPageSize = 3;
        private readonly IPersonalNoteService _personalNoteService;
        private readonly IUriService _uriService;

        public PersonalNotesController(IPersonalNoteService personalNoteService, IUriService uriService)
        {
            _personalNoteService = personalNoteService;
            _uriService = uriService;
        }

        [HttpGet("{idUser:int}")]
        public async Task<IActionResult> GetPersonalNoteList(int idUser, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, DefaultPageSize);

            var personalNoteList = await _personalNoteService.GetPersonalNotesByIdUser(idUser);
            var personalNoteWithPaging = personalNoteList
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = personalNoteList.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse(personalNoteWithPaging, validFilter, totalRecords,
                _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewPersonalNote(PersonalNote note)
        {
            await _personalNoteService.SaveNewNote(note);
            return StatusCode(201, note);
        }

        [HttpDelete("{idNote:int}")]
        public async Task<IActionResult> DeletePersonalNote(int idNote)
        {
            await _personalNoteService.DeletePersonalNote(idNote);
            return StatusCode(200);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePersonalNote(PersonalNote note)
        {
            await _personalNoteService.UpdatePersonalNote(note);
            return StatusCode(200, note);
        }
    }
}