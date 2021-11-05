using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Wrappers;

namespace MentorApp
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDTO>();
            CreateMap<Models.Profile, ProfileDTO>();
            CreateMap<TaskRequestDTO, Models.Task>();
            CreateMap<Models.Meeting, MeetingDetailDto>();
            CreateMap<Models.Meeting, MeetingHeadDto>();
            CreateMap<MeetingRequestDto, Models.Meeting>();


        }
    }
}
