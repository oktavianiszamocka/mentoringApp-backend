using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IPersonalNoteService
    {
        Task<List<PersonalNote>> GetPersonalNotesByIdUser(int IdUser);
        Task<PersonalNote> SaveNewNote(PersonalNote note);
    }
}
