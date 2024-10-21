using FlowSocial.Application.Common.DTO.Request;
using FlowSocial.Application.DTOs.Response;
using FlowSocial.Application.Services.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octagram.Application.Services;
using System.Security.Claims;

namespace FlowSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController(IStoryService storyService) : ControllerBase
    {

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<StoryDto>>> GetStoriesByUserId(string userId)
        {
            var stories = await storyService.GetStoriesByUserIdAsync(userId);
            return Ok(stories);
        }

      
        [HttpGet("following")]
      
        public async Task<ActionResult<IEnumerable<StoryDto>>> GetStoriesFromFollowing()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stories = await storyService.GetStoriesFromFollowingUsersAsync(userId);
            return Ok(stories);
        }

       
        [HttpPost]
      
        public async Task<ActionResult<StoryDto>> CreateStory([FromForm] CreateStoryRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await storyService.CreateStoryAsync(request, userId);
            return Ok(res.message);
        }

        [HttpDelete("{storyId:int}")]

        public async Task<IActionResult> DeleteStory(int storyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingStory = await storyService.GetStoryByIdAsync(storyId);
            if (existingStory == null)
            {
                return BadRequest("Story not found.");
            }

            if (existingStory.UserId != userId)
            {
                return BadRequest("You are not authorized to delete this story.");
            }

            var res=await storyService.DeleteStoryAsync(storyId, userId);
            return Ok(res.message);
        }


    }
}
