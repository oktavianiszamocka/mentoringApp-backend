using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;

namespace MentorApp.Repository
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAllMessageOfUser(int idUser);
        Task<List<Message>> GetAllMessagesOfSender(int idReceiver, int idSender);
        Task<Message> CreateNewMessage(Message message);
        Task<Message> DeleteMessage(int idMessage);
        Task<List<User>> GetReceiversList();
    }
}
