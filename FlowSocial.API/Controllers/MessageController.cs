using FlowSocial.Application.DTOs.Request;
using FlowSocial.Application.DTOs.Response;
using FlowSocial.Application.Services.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlowSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController(IMessageService _messageService) : ControllerBase
    {

        [HttpGet("{targetUserId}")]
       
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversation(string targetUserId, [FromQuery] int page = 1,[FromQuery] int pageSize = 10)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var conversation = await _messageService.GetConversion(userId, targetUserId, page, pageSize);
            return Ok(conversation);
        }


        [HttpPost]
      
        public async Task<ActionResult<Conversation>> SendDirectMessage([FromBody] SendMessageDto request)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var message = await _messageService.SendMessage(request, senderId);
            return Ok( message);
        }

        [HttpPatch("{messageId:int}/read")]
       
        public async Task<IActionResult> MarkMessageAsRead(int messageId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response=await _messageService.MarkMessageAsReadAsync(messageId, userId);
            if (response.flag)
                return Ok("Message marked as read.");
            return BadRequest(response.message);
        }


        [HttpPatch("Edit/{messageId:int}")]

        public async Task<IActionResult> EditMessage(int messageId,string Message)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _messageService.EditMessage(messageId, Message,userId);
            if (response.flag)
                return Ok(response.message);
            return BadRequest(response.message);
        }

        [HttpDelete("{messageId:int}")]

        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _messageService.DeleteMessage(messageId, userId);
            if (response.flag)
                return Ok(response.message);
            return BadRequest(response.message);
        }

    }
}
