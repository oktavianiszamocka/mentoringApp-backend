using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace MentorApp.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MentorAppContext _context;

        public MessageRepository(MentorAppContext context)
        {
            _context = context;
        }
        public async Task<List<Message>> GetAllMessageOfUser(int idUser)
        {
            return await _context.Message
                .Where(msg => msg.Receiver.Equals(idUser))
                .Include(msg => msg.ReceiverNavigation)
                .Include(msg => msg.SenderNavigation)
                .OrderByDescending(msg => msg.CreatedOn)
                .ToListAsync();
        }

        public async Task<List<Message>> GetAllMessagesOfSender(int idReceiver, int idSender)
        {
            return await _context.Message
                .Where(msg =>( msg.Receiver.Equals(idReceiver) && msg.Sender.Equals(idSender)) || msg.Sender.Equals(idReceiver) && msg.Receiver.Equals(idSender))
                .Include(msg => msg.ReceiverNavigation)
                .Include(msg => msg.SenderNavigation)
                .OrderByDescending(msg => msg.CreatedOn)
                .ToListAsync();
        }
    }
}
