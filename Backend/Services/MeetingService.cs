using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Repository;

namespace MentorApp.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public MeetingService(IMeetingRepository meetingRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }
        public async Task<MeetingDetailDto> GetMeetingById(int idMeeting)
        {
            var meeting = await _meetingRepository.GetMeetingById(idMeeting);
            var meetingDto = _mapper.Map<MeetingDetailDto>(meeting);
            if (meeting.MeetingAttendence.Count > 0)
            {
                meetingDto.MeetingAttendee = meeting.MeetingAttendence.Select(attendee => new UserAttendeeWrapper
                {
                    IdUser = (int) attendee.User,
                    IdAttendence = attendee.IdAttendence,
                    firstName = attendee.UserNavigation.FirstName,
                    lastName = attendee.UserNavigation.LastName,
                    imageUrl = attendee.UserNavigation.Avatar,
                    isAttend = attendee.IsAttend
                }).ToList();
            }
            
            return meetingDto;
        }

        public async Task<List<MeetingHeadDto>> GetMeetingByUser(int idUser, DateTime dateCalendar)
        {
            var userMeetingList = await _meetingRepository.GetMeetingByUser(idUser);
            var calendarMeeting = userMeetingList
                .Where(meeting => meeting.MeetingNavigation.MeetingDate.Equals(dateCalendar))
                .ToList();
            var userMeetingHeadDto = calendarMeeting.Select(meeting => new MeetingHeadDto
            {
                IdMeeting = meeting.MeetingNavigation.IdMeeting,
                Title = meeting.MeetingNavigation.Title,
                MeetingDate = meeting.MeetingNavigation.MeetingDate,
                StartTime = meeting.MeetingNavigation.StartTime,
                EndTime = meeting.MeetingNavigation.EndTime
            }).ToList();

            return userMeetingHeadDto;
        }


        public async Task<List<MeetingHeadDto>> GetMeetingByProject(int idProject, DateTime dateCalendar)
        {
            var meetingProjectList = await _meetingRepository.GetMeetingByProject(idProject, dateCalendar);
            var meetingHeadDtoList = _mapper.Map<List<MeetingHeadDto>>(meetingProjectList);
            return meetingHeadDtoList;
        }

        public async Task<MeetingDetailDto> CreateMeeting(MeetingRequestDto newMeetingRequestDto)
        {
            var newMeeting = _mapper.Map<Meeting>(newMeetingRequestDto);
            var insertedMeeting = await _meetingRepository.CreateMeeting(newMeeting);
            if (newMeetingRequestDto.AttendeeUsers.Count > 0)
            {
                foreach (var userId in newMeetingRequestDto.AttendeeUsers)
                {
                    var newAttendee = new MeetingAttendence
                    {
                        User = userId,
                        Meeting = insertedMeeting.IdMeeting
                    };
                    await _meetingRepository.CreateMeetingAttendence(newAttendee);
                }

            }

            var newMeetingDetailDto = await this.GetMeetingById(insertedMeeting.IdMeeting);
            return newMeetingDetailDto;

        }

        public async Task<Meeting> DeleteMeeting(int idMeeting)
        {
            return await _meetingRepository.DeleteMeeting(idMeeting);
            
        }

        public async Task<MeetingAttendence> UpdateMeetingAttendeeStatus(MeetingAttendence meetingAttendence)
        {
            return await _meetingRepository.UpdateIsAttend(meetingAttendence);
        }

        public async Task<MeetingDetailDto> UpdateMeeting(MeetingRequestDto meetingRequestDto)
        {
            var meetingModel = _mapper.Map<Meeting>(meetingRequestDto);
            var meetingToUpdate = await _meetingRepository.UpdateMeeting(meetingModel);
            if (meetingRequestDto.IsAddNewAttendee && meetingRequestDto.AttendeeToAdd.Count > 0)
            {
                foreach (var idAttendee in meetingRequestDto.AttendeeToAdd)
                {
                    var newMeetingAttende = new MeetingAttendence
                    {
                        User = idAttendee,
                        Meeting = meetingRequestDto.IdMeeting
                    };
                    await _meetingRepository.CreateMeetingAttendence(newMeetingAttende);
                }
            }

            if (meetingRequestDto.IsRemoveAttendee && meetingRequestDto.AttendeeToRemove.Count > 0)
            {
                foreach (var idAttendee in meetingRequestDto.AttendeeToRemove)
                {
                    var attendeeToRemove =
                        await _meetingRepository.GetMeetingAttendeeByIdUserAndIdMeeting(idAttendee,
                            meetingRequestDto.IdMeeting);
                    await _meetingRepository.DeleteMeetingAttendence(attendeeToRemove.IdAttendence);
                }
                
            }

            return await this.GetMeetingById(meetingToUpdate.IdMeeting);
        }
    }
}
