﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Repository;
using MentorApp.Wrappers;

namespace MentorApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<List<MessageOverviewDto>> GetAllMessageOfUser(int idUser)
        {
            var messageList = await _messageRepository.GetAllMessageOfUser(idUser);
            var messageMap = new SortedDictionary<int, Message>();

            foreach (var messageItem in messageList)
            {
                if (!messageMap.ContainsKey(messageItem.Sender))
                {
                    messageMap.Add(messageItem.Sender, messageItem);
                }
            }

            var messageOverviewDto = messageMap.Select(x => new MessageOverviewDto
            {
                SenderId = x.Key,
                Message = x.Value.Message1,
                SenderUser = new UserWrapper
                {
                    IdUser = x.Value.Sender,
                    firstName = x.Value.SenderNavigation.FirstName,
                    lastName = x.Value.SenderNavigation.LastName,
                    imageUrl = x.Value.SenderNavigation.Avatar
                }
            }).ToList();
            return messageOverviewDto;
        }

        public async Task<MessageDetailDto> GetAllMessagesOfSender(int idReceiver, int idSender)
        {
            var messageList = await _messageRepository.GetAllMessagesOfSender(idReceiver, idSender);
            var messageDetailDto=  new MessageDetailDto
            {
                SenderUser = new UserWrapper
                {
                    IdUser = messageList.FirstOrDefault().Sender,
                    firstName = messageList.FirstOrDefault().SenderNavigation.FirstName,
                    lastName = messageList.FirstOrDefault().SenderNavigation.LastName
                },
                ReceiverUser = new UserWrapper
                {
                    IdUser = messageList.FirstOrDefault().Receiver,
                    firstName = messageList.FirstOrDefault().ReceiverNavigation.FirstName,
                    lastName = messageList.FirstOrDefault().ReceiverNavigation.LastName
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
    }
}
