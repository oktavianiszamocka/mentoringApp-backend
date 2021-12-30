using MentorApp.Models;
using MentorApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class MeetingNotesService : IMeetingNotesService
    {
        private readonly IMeetingNotesRepository _meetingNotesRepository;

        public MeetingNotesService(IMeetingNotesRepository meetingNotesRepository)
        {
            _meetingNotesRepository = meetingNotesRepository;
        }

        public async Task<Note> CreateNewMeetingNote(Note note)
        {
            return await _meetingNotesRepository.CreateNewMeetingNote(note);
        }

        public async Task<Note> DeleteMeetingNote(int idNote)
        {
            return await _meetingNotesRepository.DeleteMeetingNote(idNote);
        }

        public async Task<List<Note>> GetAllNotesOfMeeting(int idMeeting)
        {
            return await _meetingNotesRepository.GetAllNotesOfMeeting(idMeeting);
        }

        public async Task<Note> UpdateMeetingNote(Note note)
        {
            return await _meetingNotesRepository.UpdateMeetingNote(note);
        }
    }
}
