using MentorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IPersonalNoteService
    {
        Task<List<PersonalNote>> GetPersonalNotesByIdUser(int IdUser);
        Task<PersonalNote> SaveNewNote(PersonalNote note);
        Task<PersonalNote> DeletePersonalNote(int PersonalNoteId);
        Task<PersonalNote> UpdatePersonalNote(PersonalNote Note);
    }
}
