
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
        private readonly IPersonalNoteService _personalNoteService;
        private readonly IUriService _uriService;

        public PersonalNotesController(IPersonalNoteService personalNoteService, IUriService uriService)
        {
            _personalNoteService = personalNoteService;
            _uriService = uriService;
        }

        [HttpGet("{IdUser:int}")]
        public async Task<IActionResult> GetPersonalNoteList(int IdUser, [FromQuery] Filter.PaginationFilter filter )
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, DefaultPageSize);

            var personalNoteList = await _personalNoteService.GetPersonalNotesByIdUser(IdUser);
            var personalNoteWithPaging =  personalNoteList
                                         .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                          .Take(validFilter.PageSize)
                                          .ToList();

            var totalRecords = personalNoteList.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse<PersonalNote>(personalNoteWithPaging, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewPersonalNote(PersonalNote note)
        {
            await _personalNoteService.SaveNewNote(note);
            return StatusCode(201, note);
        }

        [HttpDelete("{IdNote:int}")]
        public async Task<IActionResult> DeletePersonalNote(int IdNote)
        {
            await _personalNoteService.DeletePersonalNote(IdNote);
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