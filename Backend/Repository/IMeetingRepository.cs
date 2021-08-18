using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using Task = MentorApp.Models.Task;

namespace MentorApp.Repository
{
    public interface IMeetingRepository
    {
        Task<Meeting> GetMeetingById(int idMeeting);
        Task<List<MeetingAttendence>> GetMeetingByUser(int idUser);
        Task<List<Meeting>> GetMeetingByProject(int idProject, DateTime dateCalendar);

        Task<Meeting> CreateMeeting(Meeting newMeeting);

        Task<Meeting> DeleteMeeting(int idMeeting);
        Task<MeetingAttendence> DeleteMeetingAttendence(int idMeetingAttendence);

        Task<MeetingAttendence> UpdateIsAttend(MeetingAttendence meetingAttendence);
        Task<Meeting> UpdateMeeting(Meeting meeting);

        Task<MeetingAttendence> CreateMeetingAttendence(MeetingAttendence meetingAttendence);

        Task<MeetingAttendence> GetMeetingAttendeeByIdUserAndIdMeeting(int idUser, int idMeeting);



    }
}
