using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MentorApp.Repository
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly MentorAppContext _context;

        public MeetingRepository(MentorAppContext context)
        {
            _context = context;
        }

        public async Task<Meeting> GetMeetingById(int idMeeting)
        {
            var meeting = await _context.Meeting
                .Where(meeting => meeting.IdMeeting.Equals(idMeeting))
                .Include(meeting => meeting.MeetingAttendence)
                    .ThenInclude(attendee => attendee.UserNavigation)
                .FirstOrDefaultAsync();
            return meeting;
        }

        public async Task<List<MeetingAttendence>> GetMeetingByUser(int idUser)
        {
            var userMeeting = await _context.MeetingAttendence
                .Where(attende => attende.User.Equals(idUser))
                .Include(meeting => meeting.MeetingNavigation)
                .ToListAsync();

            return userMeeting;
        }

        public async Task<List<Meeting>> GetMeetingByProject(int idProject)
        {
            var projectMeetings = await _context.Meeting
                .Where(meeting => meeting.Project.Equals(idProject))
                .ToListAsync();
            return projectMeetings;
        }

        public async Task<Meeting> CreateMeeting(Meeting newMeeting)
        {
            var addedMeeting = await _context.Meeting.AddAsync(newMeeting);
            await _context.SaveChangesAsync();
            return addedMeeting.Entity;

        }
        public async Task<MeetingAttendence> CreateMeetingAttendence(MeetingAttendence meetingAttendence)
        {
            var addedMeetingAttendence = await _context.MeetingAttendence.AddAsync(meetingAttendence);
            await _context.SaveChangesAsync();
            return addedMeetingAttendence.Entity;
        }

        public async Task<MeetingAttendence> GetMeetingAttendeeByIdUserAndIdMeeting(int idUser, int idMeeting)
        {
            return await _context.MeetingAttendence
                .Where(attendee => attendee.User.Equals(idUser) && attendee.Meeting.Equals(idMeeting))
                .FirstOrDefaultAsync();
        }

        public async Task<Meeting> DeleteMeeting(int idMeeting)
        {
            var deletedMeetig = await _context.Meeting.FindAsync(idMeeting);
            _context.Meeting.Remove(deletedMeetig);
            await _context.SaveChangesAsync();
            return deletedMeetig;

        }

        public async Task<MeetingAttendence> DeleteMeetingAttendence(int idMeetingAttendence)
        {
            var meetingAttendenceToRemove = await _context.MeetingAttendence.FindAsync(idMeetingAttendence);
            _context.MeetingAttendence.Remove(meetingAttendenceToRemove);
            await _context.SaveChangesAsync();
            return meetingAttendenceToRemove;
        }

        public async Task<MeetingAttendence> UpdateIsAttend(MeetingAttendence meetingAttendence)
        {
            var meetingAttendeeToUpdate = await _context.MeetingAttendence.FindAsync(meetingAttendence.IdAttendence);
            meetingAttendeeToUpdate.IsAttend = meetingAttendence.IsAttend;
            _context.MeetingAttendence.Update(meetingAttendeeToUpdate);
            await _context.SaveChangesAsync();
            return meetingAttendeeToUpdate;
        }

        public async Task<Meeting> UpdateMeeting(Meeting meeting)
        {
            var updateMeeting = await _context.Meeting.FindAsync(meeting.IdMeeting);
            updateMeeting.Title = meeting.Title;
            updateMeeting.Description = meeting.Description;
            updateMeeting.Location = meeting.Location;
            updateMeeting.MeetingDate = meeting.MeetingDate;
            updateMeeting.StartTime = meeting.StartTime;
            updateMeeting.EndTime= meeting.EndTime;
            _context.Meeting.Update(updateMeeting);
            await _context.SaveChangesAsync();
            return updateMeeting;
        }

      
    }
}
