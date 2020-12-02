using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public class PersonalNoteRepository : IPersonalNoteRepository
    {
        private readonly MentorAppContext _context;

        public PersonalNoteRepository(MentorAppContext context)
        {
            _context = context;
        }

        public async Task<PersonalNote> DeletePersonalNote(int PersonalNoteId)
        {
            var note = await _context.PersonalNote.FindAsync(PersonalNoteId);
            _context.PersonalNote.Remove(note);
            await _context.SaveChangesAsync();
            return note;
            
        }

        public async Task<List<PersonalNote>> GetPersonalNotesByUser(int IdUser)
        {
            return await _context.PersonalNote
                            .Where(note => note.User.Equals(IdUser))
                            .OrderByDescending(Note => Note.LastModified)
                        .ToListAsync();
        }

        public async Task<PersonalNote> SaveNewPersonalNote(PersonalNote note)
        {
            _context.PersonalNote.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<PersonalNote> UpdatePersonalNote(PersonalNote Note)
        {
            _context.Entry(Note).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Note;
            
        }
    }
}
