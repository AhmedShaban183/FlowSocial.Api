using FlowSocial.Application.Common.DTO.Request.Account;
using FlowSocial.Application.Services.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FlowSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthencationController : ControllerBase
    {
        private readonly IAccount _account;
        public AuthencationController(IAccount _account)
        {
            this._account = _account;
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            await _account.ConfirmEmail(userId, token);
            return Ok();
        }





        [HttpGet("sendEmail")]
        public async Task<IActionResult> sentEmailConfirm(string id)
        {
            await _account.SendEmail(id);
            return Ok();
        }



        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string userId, string token, string password)
        {
            {
                var result = await _account.ResetPassword(userId, token, password);
                if (result.flag)
                {
                    return Ok(result.message);
                }
                return BadRequest(result.message);
            }




        }



        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([Required] string email)
        {
            await _account.ForgetPassword(email);
            return Ok();
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {

            if (ModelState.IsValid)
            {
                var res = await _account.ChangePassword(changePasswordDTO);

                return Ok(res.message);
            }
            return BadRequest(ModelState);



        }
    }
}
