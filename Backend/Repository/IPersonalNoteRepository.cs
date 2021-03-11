using MentorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IPersonalNoteRepository
    {
        Task<List<PersonalNote>> GetPersonalNotesByUser(int IdUser);
        Task<PersonalNote> SaveNewPersonalNote(PersonalNote note);
        Task<PersonalNote> DeletePersonalNote(int PersonalNoteId);
        Task<PersonalNote> UpdatePersonalNote(PersonalNote Note);
    }
}
