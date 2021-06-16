using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Models;

namespace MentorApp.Services
{
    public interface IMessageService
    {
        Task<List<MessageOverviewDto>> GetAllMessageOfUser(int idUser);
        Task<MessageDetailDto> GetAllMessagesOfSender(int idReceiver, int idSender);
        Task<Message> CreateNewMessage(Message message);
        Task<Message> DeleteMessage(int idMessage);
    }
}
