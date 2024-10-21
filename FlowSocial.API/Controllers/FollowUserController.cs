using FlowSocial.Application.DTOs.Response.Account;
using FlowSocial.Application.Services.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlowSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUserController : ControllerBase
    {
        private readonly IFollowService followService;

        public FollowUserController(IFollowService followService)
        {
            this.followService = followService;
        }

        [HttpPost("Follow/{followingId}")]
        public async Task<IActionResult> FollowUser(string followingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == followingId)
                return BadRequest("You cannot follow yourself.");

            await followService.FollowUserAsync(userId, followingId);
            return Ok("User followed successfully.");
        }

       
        [HttpDelete("follow/{followingId}")]
        
        public async Task<IActionResult> UnfollowUser(string followingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == followingId)
                return BadRequest("You cannot unfollow yourself.");

           var res= await followService.UnfollowUserAsync(userId, followingId);
            if (res.flag)
            {
                return Ok("User unfollowed successfully.");
            }
            else
            {
                return BadRequest(res.message); 
            }
        }

        [HttpGet("{userId}/followers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetFollowers(string userId)
        {
            var followers = await followService.GetFollowersAsync(userId);
            return Ok(followers);
        }

       
        [HttpGet("{userId}/following")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetFollowing(string userId)
        {
            var following = await followService.GetFollowingAsync(userId);
            return Ok(following);
        }





    }
}
