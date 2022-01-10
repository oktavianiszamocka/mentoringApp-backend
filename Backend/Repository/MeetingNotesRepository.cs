using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public class MeetingNotesRepository : IMeetingNotesRepository
    {
        private readonly MentorAppContext _context;

        public MeetingNotesRepository(MentorAppContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetAllNotesOfMeeting(int idMeeting)
        {
            return await _context.Note
                        .Where(note => note.Meeting.Equals(idMeeting))
                        .Include(note => note.AuthorNavigation)
                        .OrderBy(note => note.CreatedOn)
                        .ToListAsync();
        }

        public async Task<Note> CreateNewMeetingNote(Note note)
        {
            var newNote = await _context.Note.AddAsync(note);
            await _context.SaveChangesAsync();
            return newNote.Entity;
        }

        public async Task<Note> DeleteMeetingNote(int idNote)
        {
            var note = await _context.Note.FindAsync(idNote);
            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<Note> UpdateMeetingNote(Note note)
        {
            var noteToUpdate = await _context.Note.FindAsync(note.IdNote);
            noteToUpdate.Subject = note.Subject;
            noteToUpdate.Note1 = note.Note1;
            noteToUpdate.LastModified = note.LastModified;


            _context.Note.Update(noteToUpdate);
            await _context.SaveChangesAsync();
            return noteToUpdate;
        }
    }
}
