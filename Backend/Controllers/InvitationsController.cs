using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Filter;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Controllers
{
    [Route("api/invitations")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private const int DefaultPageSize = 10;
        private readonly IInvitationService _invitationService;
        private readonly IUriService _uriService;

        public InvitationsController(IInvitationService invitationService, IUriService uriService)
        {
            _invitationService = invitationService;
            _uriService = uriService;

        }

        [HttpGet("{idUser:int}")]
        public async Task<IActionResult> GetShortInvitations(int idUser, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, 5);

            var invitations = await _invitationService.GetInvitationByUser(idUser);
            var invitationsWithPaging = invitations
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = invitations.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse(invitationsWithPaging, validFilter, totalRecords,
                _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("full/{idUser:int}")]
        public async Task<IActionResult> GetAllInvitations(int idUser, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, DefaultPageSize);

            var invitations = await _invitationService.GetInvitationByUser(idUser);
            var invitationsWithPaging = invitations
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = invitations.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse(invitationsWithPaging, validFilter, totalRecords,
                _uriService, route);
            return Ok(pagedReponse);
        }


        [HttpGet("member/{idProject:int}")]
        public async Task<IActionResult> GetProjectMemberInvitationListByIdProject(int idProject)
        {
            var invitationProjectMemberByProject = await _invitationService.GetInvitationProjectMemberByProject(idProject);
            return Ok(new Response<List<InvitationProjectDTO>>(invitationProjectMemberByProject));
        }

        [HttpGet("promotor/{idProject:int}")]
        public async Task<IActionResult> GetProjectPromotorInvitationListByIdProject(int idProject)
        {
            var invitationProjectPromotorByProject = await _invitationService.GetInvitationProjectPromoterByProject(idProject);
            return Ok(new Response<List<String>>(invitationProjectPromotorByProject));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateInvitations(Invitation invitationToUpdate)
        {
            var updatedInvitation = await _invitationService.UpdateInvitation(invitationToUpdate);
            return StatusCode(200, updatedInvitation);
        }
    }
}