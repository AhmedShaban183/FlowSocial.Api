using FlowSocial.Application.DTOs.Request;
using FlowSocial.Application.DTOs.Response;


namespace FlowSocial.Application.Services.InterfaceService
{
    public interface IMessageService
    {

        Task<IEnumerable<Conversation>> GetConversion(string UserID1, string UserID2, int page = 0, int pageSize = 0);
        Task<Conversation> SendMessage(SendMessageDto messageDto, string senderId);
        Task<GeneralResponse> DeleteMessage(int messageID, string userId);
        Task<GeneralResponse> EditMessage(int messageID, string Message, string userId);
        Task<GeneralResponse> MarkMessageAsReadAsync(int messageId, string userId);


    }
}
