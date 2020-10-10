using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MentorApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly s17874Context _context;

        public MessagesController(s17874Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Message message)
        {
            _context.Message.Add(message);
            await _context.SaveChangesAsync();
            return StatusCode(201, message);
        }

        [HttpGet("{ReceiverId:int}/{SenderId:int}/")]
        public async Task<IActionResult> GetMessageByReceiverAndSender(int ReceiverId, int SenderId)
        {
            return Ok((await _context.Message
                            .Where(msg =>( msg.Receiver.Equals(ReceiverId) && msg.Sender.Equals(SenderId)) || 
                            (msg.Receiver.Equals(SenderId) && msg.Sender.Equals(ReceiverId)))
                            .OrderByDescending(msg => msg.CreatedOn)
                            .ToListAsync()
                     )); 
        }


        [HttpGet("{ReceiverId:int}")]
        public async Task<IActionResult> GetSenderUsersByReceiver(int ReceiverId)
        {
            var message = (from msg in _context.Message
                           where msg.Receiver == ReceiverId
                       join user in _context.User on msg.Sender equals user.IdUser
                       select new
                       {
                           Name  = user.FirstName + ' ' + user.LastName
                          
                       })
                       .ToListAsync();

            return Ok((await message)); 
        }
          
    }

}