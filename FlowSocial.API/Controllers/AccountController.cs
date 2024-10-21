
using FlowSocial.Application.Common.DTO.Request.Account;
using FlowSocial.Application.DTOs.Request.Account;
using FlowSocial.Application.DTOs.Response.Account;
using FlowSocial.Application.Services.Implement;
using FlowSocial.Application.Services.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _account;
        public AccountController(IAccount _account)
        {
            this._account = _account;
        }

        [HttpPost("identity/create")]
        public async Task<IActionResult> CreateAccount([FromForm]CreateAccountDTO newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _account.CreateAccountAsync(newAccount));

        }
        [HttpPost("identity/login")]
        public async Task<IActionResult> Login(LoginDTO signAcc)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _account.LoginAccountAsync(signAcc);
            if (!response.flag) return BadRequest(response.message);

            return Ok(response);

        }
        [HttpPost("identity/refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTockenDto refreshTockenDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _account.RefreshTokenAsync(refreshTockenDto);
            if (!response.flag) return BadRequest(response.message);

            return Ok(response);

        }



        [HttpGet("/NewAdmin")]
        public async Task<IActionResult> CreateAdmin()
        {

            await _account.CreateAdmin();
            return Ok();

        }

        [HttpGet("identity/user-with-role")]
        public async Task<IActionResult> GetUserWithRoles()
        {


            return Ok(await _account.GetUsersWithRoleAsync());

        }

     
     

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var res = await _account.RemoveUser(id);
            if (res.flag)
            {
                return Ok(res.message);
            }
            return BadRequest(res.message);
        }

        [HttpGet("LogOut")]
        public async Task<IActionResult> Logout()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _account.LogOut(id);
            return Ok();
        }
        [HttpGet("userID")]
        public async Task<IActionResult> GetFollowing()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return BadRequest("You need To sign In");
            
            return Ok(userId);
        }



    }


}
