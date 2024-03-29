﻿using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IMeetingNotesRepository
    {
        Task<List<Note>> GetAllNotesOfMeeting(int idMeeting);
        Task<Note> CreateNewMeetingNote(Note note);
        Task<Note> UpdateMeetingNote(Note note);
        Task<Note> DeleteMeetingNote(int idNote);

    }
}
