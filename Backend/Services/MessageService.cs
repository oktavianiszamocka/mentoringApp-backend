using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;
using MentorApp.DTOs.Responses;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Repository;
using MentorApp.Wrappers;

namespace MentorApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;


        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }
        public async Task<List<MessageOverviewDto>> GetAllMessageOfUser(int idUser)
        {
            var messageList = await _messageRepository.GetAllMessageOfUser(idUser);
            var messageMap = new SortedDictionary<int, Message>();

            foreach (var messageItem in messageList)
            {
                if (messageItem.Sender.Equals(idUser))
                {
                    if (!messageMap.ContainsKey(messageItem.Receiver))
                    {
                        messageMap.Add(messageItem.Receiver, messageItem);
                    }

                }
                else
                {
                    if (!messageMap.ContainsKey(messageItem.Sender))
                    {
                        messageMap.Add(messageItem.Sender, messageItem);
                    }
                }
                
            }

            var messageDTOMap = new SortedDictionary<int, MessageOverviewDto>();
            foreach (var userId in messageMap.Keys)
            {
                var userTarget = await _userRepository.GetUserById(userId);
                var msg = messageMap[userId];
                messageDTOMap.Add(userId, new MessageOverviewDto
                {
                    SenderId = userId,
                    Message = msg.Message1,
                    LastMessage = msg.CreatedOn,
                    SenderUser = new UserWrapper
                    {
                        IdUser = userId,
                        firstName = userTarget.FirstName,
                        lastName = userTarget.LastName,
                        imageUrl = userTarget.Avatar
                    }

                });
            }

            return  messageDTOMap.Values.ToList();

        }

        public async Task<List<ReceiverListDTO>> GetReceiverList(String search)
        {
            var receiverList = await _messageRepository.GetReceiversList(search);

            var receivers = receiverList.Select(user => new ReceiverListDTO
            {
                IdReceiver = user.IdUser,
                FirstName = user.FirstName,
                LastName = user.LastName
            }).ToList();

            return receivers;

        }
        public async Task<MessageDetailDto> GetAllMessagesOfSender(int idReceiver, int idSender, int currentUser)
        {
            var messageList = await _messageRepository.GetAllMessagesOfSender(idReceiver, idSender);
            var senderUser = await _userRepository.GetUserById(currentUser);
            var receiverUser = await _userRepository.GetUserById(idReceiver);

            if (messageList.Count > 0)
            {
                var messageDetailDto = new MessageDetailDto
                {
                    SenderUser = new UserWrapper
                    {
                        IdUser = senderUser.IdUser,
                        firstName = senderUser.FirstName,
                        lastName = senderUser.LastName,
                        imageUrl = senderUser.Avatar
                    },
                    ReceiverUser = new UserWrapper
                    {
                        IdUser = receiverUser.IdUser,
                        firstName = receiverUser.FirstName,
                        lastName = receiverUser.LastName,
                        imageUrl = receiverUser.Avatar
                    },
                    Messages = messageList.Select(msg => new MessageDto
                    {
                        IdMessage = msg.IdMessage,
                        Sender = msg.Sender,
                        Receiver = msg.Receiver,
                        Message1 = msg.Message1,
                        CreatedOn = msg.CreatedOn
                    }).ToList()
                };
                return messageDetailDto;
            }
            else
            {

                throw new HttpResponseException("No message founds");
            }
           
        }

        public async Task<Message> CreateNewMessage(Message message)
        {
            var newMessage = await _messageRepository.CreateNewMessage(message);
            return newMessage;
        }

        public async Task<Message> DeleteMessage(int idMessage)
        {
            var message = await _messageRepository.DeleteMessage(idMessage);
            return message;
        }
    }
}
