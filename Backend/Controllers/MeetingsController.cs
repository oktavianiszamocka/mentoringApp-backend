using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Controllers
{
    [Route("api/meetings")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingsController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [Authorize]
        [HttpGet("{idMeeting:int}")]
        public async Task<IActionResult> GetMeetingById(int idMeeting)
        {
            var meetingDetail = await _meetingService.GetMeetingById(idMeeting);
            return Ok(new Response<MeetingDetailDto>(meetingDetail));
        }

        [Authorize]
        [HttpGet("user/{idUser:int}")]
        public async Task<IActionResult> GetMeetingListByIdUser(int idUser, [FromQuery(Name = "date")] DateTime dateCalendar)
        {
            var userMeetingList = await _meetingService.GetMeetingByUser(idUser, dateCalendar);
            return Ok(new Response<List<MeetingHeadDto>>(userMeetingList));
        }

        [Authorize]
        [HttpGet("project/{idProject:int}")]
        public async Task<IActionResult> GetMeetingListByIdProject(int idProject, [FromQuery(Name = "date")] DateTime dateCalendar)
        {
            var projectMeetingList = await _meetingService.GetMeetingByProject(idProject, dateCalendar);
            return Ok(new Response<List<MeetingHeadDto>>(projectMeetingList));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNewMeeting(MeetingRequestDto newMeetingDto)
        {
            try
            {
                var newMeeting = await _meetingService.CreateMeeting(newMeetingDto);

                return StatusCode(201, newMeeting);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }

        [Authorize]
        [HttpDelete("{idMeeting:int}")]
        public async Task<IActionResult> DeleteMeeting(int idMeeting)
        {
            await _meetingService.DeleteMeeting(idMeeting);
            return StatusCode(200);
        }

        [Authorize]
        [HttpPatch("attendee/update-status")]
        public async Task<IActionResult> UpdateAttendeeStatus(MeetingAttendence meetingAttendence)
        {
            var updateAttendee = await _meetingService.UpdateMeetingAttendeeStatus(meetingAttendence);
            return StatusCode(200, updateAttendee);
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateMeeting(MeetingRequestDto meetingRequestDto)
        {
            try
            {
                var updateMeeting = await _meetingService.UpdateMeeting(meetingRequestDto);
                return StatusCode(200, updateMeeting);

            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }
    }
}