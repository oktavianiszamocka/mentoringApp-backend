using MentorApp.Models;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;

namespace MentorApp.Controllers
{
    [Authorize]
    [Route("api/meeting-notes")]
    [ApiController]
    public class MeetingNotesController : ControllerBase
    {
        private readonly IMeetingNotesService _meetingNotesService;

        public MeetingNotesController(IMeetingNotesService meetingNotesService)
        {
            _meetingNotesService = meetingNotesService;
        }

        [HttpGet("{idMeeting:int}")]
        public async Task<IActionResult> GetAllNotesOfMeeting(int idMeeting)
        {
            var notes = await _meetingNotesService.GetAllNotesOfMeeting(idMeeting);
            return Ok(new Response<List<MeetingNoteResponseDTO>>(notes));
        }


        [HttpPost]
        public async Task<IActionResult> SaveNewMeetingNote(Note newNote)
        {
            var newNoteInserted = await _meetingNotesService.CreateNewMeetingNote(newNote);
            return StatusCode(201, newNoteInserted);
        }

        
        [HttpPatch]
        public async Task<IActionResult> UpdateMeetingNote(Note note)
        {
            var updatedNote= await _meetingNotesService.UpdateMeetingNote(note);
            return StatusCode(200, updatedNote);
        }

        [HttpDelete("{idNote:int}")]
        public async Task<IActionResult> DeleteMeetingNote(int idNote)
        {
            await _meetingNotesService.DeleteMeetingNote(idNote);
            return StatusCode(200);
        }
    }
}
