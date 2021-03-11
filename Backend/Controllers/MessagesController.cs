using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorApp.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MentorAppContext _context;

        public MessagesController(MentorAppContext context)
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

        [HttpGet("{receiverId:int}/{senderId:int}/")]
        public async Task<IActionResult> GetMessageByReceiverAndSender(int receiverId, int senderId)
        {
            return Ok(await _context.Message
                .Where(msg => msg.Receiver.Equals(receiverId) && msg.Sender.Equals(senderId) ||
                              msg.Receiver.Equals(senderId) && msg.Sender.Equals(receiverId))
                .OrderByDescending(msg => msg.CreatedOn)
                .ToListAsync()
            );
        }


        [HttpGet("{receiverId:int}")]
        public async Task<IActionResult> GetSenderUsersByReceiver(int receiverId)
        {
            var message = (from msg in _context.Message
                    where msg.Receiver == receiverId
                    join user in _context.User on msg.Sender equals user.IdUser
                    select new
                    {
                        Name = user.FirstName + ' ' + user.LastName
                    })
                .ToListAsync();

            return Ok(await message);
        }
    }
}