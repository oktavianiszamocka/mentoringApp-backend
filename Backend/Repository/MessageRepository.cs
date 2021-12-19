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
    
        public async Task<List<User>> GetReceiversList(String search)
        {
            return await _context.User
                .Where(user => user.FirstName.Contains(search) || user.LastName.Contains(search))
                .Take(10)
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

        public async Task<Message> CreateNewMessage(Message message)
        {
            _context.Message.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<Message> DeleteMessage(int idMessage)
        {
            var message = await _context.Message.FindAsync(idMessage);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return message;
        }

    }
}
