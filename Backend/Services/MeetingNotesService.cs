using MentorApp.Models;
using MentorApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MentorApp.DTOs.Responses;

namespace MentorApp.Services
{
    public class MeetingNotesService : IMeetingNotesService
    {
        private readonly IMeetingNotesRepository _meetingNotesRepository;
        private readonly IMapper _mapper;

        public MeetingNotesService(IMeetingNotesRepository meetingNotesRepository, IMapper mapper)
        {
            _meetingNotesRepository = meetingNotesRepository;
            _mapper = mapper;
        }

        public async Task<Note> CreateNewMeetingNote(Note note)
        {
            return await _meetingNotesRepository.CreateNewMeetingNote(note);
        }

        public async Task<Note> DeleteMeetingNote(int idNote)
        {
            return await _meetingNotesRepository.DeleteMeetingNote(idNote);
        }

        public async Task<List<MeetingNoteResponseDTO>> GetAllNotesOfMeeting(int idMeeting)
        {
            var meetingNotes = await _meetingNotesRepository.GetAllNotesOfMeeting(idMeeting);
            if (meetingNotes.Count > 0)
            {
                var meetingNotesDtos = _mapper.Map<List<MeetingNoteResponseDTO>>(meetingNotes);
                
                return meetingNotesDtos;
            }

            return new List<MeetingNoteResponseDTO>();

        }

        public async Task<Note> UpdateMeetingNote(Note note)
        {
            return await _meetingNotesRepository.UpdateMeetingNote(note);
        }
    }
}
