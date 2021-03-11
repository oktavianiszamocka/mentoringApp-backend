using MentorApp.DTOs.Responses;
using MentorApp.Repository;
using MentorApp.Wrappers;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class ProjectPromoterService : IProjectPromoterService
    {

        private readonly IProjectPromotersRepository _projectPromotersRepository;

        public ProjectPromoterService(IProjectPromotersRepository projectPromotersRepository)
        {
            _projectPromotersRepository = projectPromotersRepository;
        }

        public async Task<ProjectPromotersDTO> GetProjectPromoters(int IdProject)
        {
            var projectDB = await _projectPromotersRepository.GetProjectPromoters(IdProject);

            var projectPromoterDTO = new ProjectPromotersDTO
            {
                MainMentor = new UserWrapper
                {
                    IdUser = (int)projectDB.Superviser,
                    firstName = projectDB.SuperviserNavigation.FirstName,
                    lastName = projectDB.SuperviserNavigation.LastName,
                    imageUrl = projectDB.SuperviserNavigation.Avatar
                },
                AdditionalMentors = projectDB.ProjectPromoter
                                    .Where(promoter => !promoter.User.Equals(projectDB.Superviser))
                                    .Select(promoter => new UserWrapper
                                    {
                                        IdUser = promoter.UserNavigation.IdUser,
                                        firstName = promoter.UserNavigation.FirstName,
                                        lastName = promoter.UserNavigation.LastName,
                                        imageUrl = promoter.UserNavigation.Avatar
                                    }).ToList()

            };



            return projectPromoterDTO;
        }
    }
}
