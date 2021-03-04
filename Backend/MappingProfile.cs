using MentorApp.DTOs.Responses;
using MentorApp.Models;

namespace MentorApp
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDTO>();
            CreateMap<Models.Profile, ProfileDTO>();


        }
    }
}
