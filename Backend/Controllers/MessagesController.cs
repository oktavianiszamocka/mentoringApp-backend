﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Persistence;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorApp.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize]
        [HttpGet("detail")]
        public async Task<IActionResult> GetMessageByReceiverAndSender([FromQuery(Name = "receiver")] int receiverId, [FromQuery(Name = "sender")] int senderId, [FromQuery(Name = "current-user")] int currentUser)
        {
            try
            {
                var msgDetail = await _messageService.GetAllMessagesOfSender(receiverId, senderId, currentUser);
                return Ok(new Response<MessageDetailDto>(msgDetail));
            }
            catch (HttpResponseException exception)
            {
                return StatusCode(500, exception.Value);
            }

        }

        [Authorize]
        [HttpGet("receiverList")]
        public async Task<IActionResult> GetReceiverList([FromQuery(Name = "search")] string search)
        {
            var receiverList = await _messageService.GetReceiverList(search);

            return Ok(new Response<List<ReceiverListDTO>>(receiverList));
        }

        [Authorize]
        [HttpGet("{receiverId:int}")]
        public async Task<IActionResult> GetMessageByReceiverId(int receiverId)
        {
            var messageOverview = await _messageService.GetAllMessageOfUser(receiverId);

            return Ok(new Response<List<MessageOverviewDto>>(messageOverview));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            await _messageService.CreateNewMessage(message);
            return StatusCode(201, message);
        }

        [Authorize]
        [HttpDelete("{idMessage:int}")]
        public async Task<IActionResult> DeleteMessage(int idMessage)
        {
            await _messageService.DeleteMessage(idMessage);
            return StatusCode(200);
        }
    }
}