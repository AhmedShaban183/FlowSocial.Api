using FlowSocial.Application.DTOs.Request;
using FlowSocial.Application.DTOs.Response;
using FlowSocial.Application.DTOs.Response.Account;
using FlowSocial.Application.Services.InterfaceService;
using FlowSocial.Domain.InterfaceRepository;
using FlowSocial.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Application.Services.Implement
{
    public class MessageService(IMessageRepository messageRepository) : IMessageService
    {
        public async Task<GeneralResponse> DeleteMessage(int messageID,string userId)
        {
            var mess = messageRepository.GetByIdAsync((x) => x.Id == messageID).FirstOrDefault();
            if (mess?.SenderId == userId)
            {
                
                await messageRepository.DeleteAsync(mess);
                return new GeneralResponse(true, $"message  Deleted");
            }

            return new GeneralResponse(false, "You are not authorized to Delete this message .");
        }

        public async Task<GeneralResponse> EditMessage(int messageID, string Message,string userId)
        {
            var mess = messageRepository.GetByIdAsync((x) => x.Id == messageID).FirstOrDefault();
            if (mess?.SenderId == userId )
            {
                mess.Content = Message;
                await messageRepository.UpdateAsync();
                return new GeneralResponse(true, $"message is now {mess.Content}");
            }
           
            return new GeneralResponse(false, "You are not authorized to Edit  this message.");
        }

        public async Task<IEnumerable<Conversation>> GetConversion(string UserID1, string UserID2, int page = 0, int pageSize = 0)
        {
            IEnumerable<DirectMessage> con;
            if(page!=0 && pageSize != 0)
            {
               con=  await messageRepository.GetConversationAsync(UserID1, UserID2, page, pageSize);
            }
            else
            {
                con = await messageRepository.GetConversion(UserID1, UserID2);

            }
            var converationDto = new List<Conversation>();

            foreach (var item in con)
            {
                var itemDto = new Conversation()
                {
                    Content = item.Content,
                    CreatedAt = item.CreatedAt,
                    Id = item.Id,
                    IsRead = item.IsRead,
                    ReceiverId = item.ReceiverId,
                    SenderId = item.SenderId

                };
                itemDto.Sender = new UserDto()
                {
                    Id=item.SenderId,
                    Bio=item.Sender.Bio,
                    Username=item.Sender.UserName,
                    ProfileImageUrl=item.Sender.ProfileImageUrl
                };
                itemDto.Receiver = new UserDto()
                {
                    Id = item.ReceiverId,
                    Bio = item.Receiver.Bio,
                    Username = item.Receiver.UserName,
                    ProfileImageUrl = item.Receiver.ProfileImageUrl
                };
                converationDto.Add(itemDto);
            }
            return converationDto;
        }

        public async Task<GeneralResponse> MarkMessageAsReadAsync(int messageId, string userId)
        {
            var mess=messageRepository.GetByIdAsync((x) => x.Id == messageId).FirstOrDefault();
            if (mess?.SenderId != userId && mess?.ReceiverId != userId)
            {
                return new GeneralResponse(false,"You are not authorized to mark this message as read.");
            }
            mess.IsRead = true;
            await messageRepository.UpdateAsync();
            return new GeneralResponse(true,"message is reading now");

        }

        public async Task<Conversation> SendMessage(SendMessageDto messageDto, string senderId)
        {
            var message = new DirectMessage
            {
                SenderId = senderId,
                ReceiverId = messageDto.ReceiverId,
                Content = messageDto.Message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await messageRepository.AddAsync(message);


            return new Conversation
            {
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                IsRead = message.IsRead,
                Id = message.Id,
                ReceiverId = message.ReceiverId,
                SenderId = message.SenderId

             };
        }
    }
}
