using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using Task = System.Threading.Tasks.Task;

namespace MentorApp.Services
{
    public interface IMeetingService
    {
        Task<MeetingDetailDto> GetMeetingById(int idMeeting);
        Task<List<MeetingHeadDto>> GetMeetingByUser(int idUser);
        Task<List<MeetingHeadDto>> GetMeetingByProject(int idProject);
        Task<MeetingDetailDto> CreateMeeting(MeetingRequestDto newMeetingRequestDto);

        Task<Meeting> DeleteMeeting(int idMeeting);
        Task<MeetingAttendence> UpdateMeetingAttendeeStatus(MeetingAttendence meetingAttendence);

        Task<MeetingDetailDto> UpdateMeeting(MeetingRequestDto meetingRequestDto);
    }
}
