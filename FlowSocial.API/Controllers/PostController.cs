using FlowSocial.Application.Common.DTO.Request;
using FlowSocial.Application.Services.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService=postService;
        }
        [HttpGet ("Posts")]
      //  [Authorize]
        public async Task<IActionResult> GetALL() 
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var posts=await _postService.GetALLPostAsync(UserID);
          
            return Ok(posts);
        }

        [HttpGet("{id:int}", Name = "GetOnePostRoute")]
       
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPostService(id);
            return Ok(post);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm]CreatePostDTO post)
        {
            if(ModelState.IsValid)
            {
                var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return BadRequest("Not Authorized");
                }
             int id=  await _postService.createPost(post, userId);
                 
            
                string url = Url.Link("GetOnePostRoute", new { id = id });
                return Created(url, post);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> Update(int Id,[FromForm]CreatePostDTO post)
        {
            if (ModelState.IsValid)
            {
               await _postService.updatePost(Id, post);
               
                string url = Url.Link("GetOnePostRoute", new { id = Id });
                return Created(url, post);
               
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            
                try
                {
                  var found = await _postService.Remove(id);
                 if (found) 
                   return StatusCode(204, "Record Remove Success");
                 return BadRequest("Invaild id");
                 }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            
           
        }
        [HttpPost("{postId:int}/like")]

        public async Task<IActionResult> Like(int postId)
        {
            string userId = GetCurrentUserId();
           var res= await _postService.Like(userId,postId);
            if (res.flag)
            {
            return Ok("Post liked successfully.");
                
            }

            return BadRequest(res.message);
        }
        [HttpDelete("{postId:int}/like")]
        public async Task<IActionResult> UnLike(int postId)
        {
            string userId = GetCurrentUserId();
            var res = await _postService.UnLike(userId, postId);
            if (res.flag)
            {
                return Ok("Post unliked successfully.");

            }
            return BadRequest(res.message);

        }

        [HttpPost("{postId:int}/comments")]
       // [Authorize("User")]
        public async Task<IActionResult> CreateComment(int postId, [FromBody] string request)
        {
            var userId = GetCurrentUserId();
            var found = await _postService.CreateCommentAsync(postId, request, userId);
            if(found)
             return Created();
           
            return BadRequest("Post Not Found");

        }

        [HttpDelete("{postId:int}/comments/{commentId:int}")]
       // [Authorize("User")]
        public async Task<IActionResult> DeleteComment(int postId, int commentId)
        {
            var userId = GetCurrentUserId();
            var found=await _postService.DeleteCommentAsync(commentId, userId);
            if(found)
                return NoContent();
            return BadRequest("Post Not Found");
        }


        [NonAction]
        private string GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null )
            {
                throw new UnauthorizedAccessException("User ID not found in claims.");
            }
            return userIdClaim;
        }
    }


}




