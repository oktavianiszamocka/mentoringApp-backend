using MentorApp.Models;
using MentorApp.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class PersonalNoteService : IPersonalNoteService
    {

        private readonly IPersonalNoteRepository _personalNoteRepository;

        public PersonalNoteService(IPersonalNoteRepository personalNoteRepository)
        {
            _personalNoteRepository = personalNoteRepository;
        }

        public async Task<PersonalNote> DeletePersonalNote(int PersonalNoteId)
        {
            return await _personalNoteRepository.DeletePersonalNote(PersonalNoteId);
        }

        public async Task<List<PersonalNote>> GetPersonalNotesByIdUser(int IdUser)
        {
            return await _personalNoteRepository.GetPersonalNotesByUser(IdUser);
        }

        public async Task<PersonalNote> SaveNewNote(PersonalNote note)
        {
            return await _personalNoteRepository.SaveNewPersonalNote(note);
        }

        public async Task<PersonalNote> UpdatePersonalNote(PersonalNote Note)
        {
            return await _personalNoteRepository.UpdatePersonalNote(Note);
        }
    }
}
