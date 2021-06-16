using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Persistence;
using MentorApp.Services;
using MentorApp.Wrappers;
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

        /*
        [HttpPost]
        public async Task<IActionResult> CreatePost(Message message)
        {
            _context.Message.Add(message);
            await _context.SaveChangesAsync();
            return StatusCode(201, message);
        }
        */

        [HttpGet("detail")]
        public async Task<IActionResult> GetMessageByReceiverAndSender([FromQuery(Name = "receiver")] int receiverId, [FromQuery(Name = "sender")] int senderId)
        {
            var msgDetail = await _messageService.GetAllMessagesOfSender(receiverId, senderId);
            return Ok(new Response<MessageDetailDto>(msgDetail));

        }


        [HttpGet("{receiverId:int}")]
        public async Task<IActionResult> GetMessageByReceiverId(int receiverId)
        {
            var messageOverview = await _messageService.GetAllMessageOfUser(receiverId);

            return Ok(new Response<List<MessageOverviewDto>>(messageOverview));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            await _messageService.CreateNewMessage(message);
            return StatusCode(201, message);
        }

        [HttpDelete("{idMessage:int}")]
        public async Task<IActionResult> DeleteMessage(int idMessage)
        {
            await _messageService.DeleteMessage(idMessage);
            return StatusCode(200);
        }
    }
}