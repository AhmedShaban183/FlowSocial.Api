using FlowSocial.Application.DTOs.Request.Account;
using FlowSocial.Application.Services.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IAccount _account;
        public RoleController(IAccount _account)
        {
            this._account = _account;
        }
        [HttpPost("identity/role/create")]
        public async Task<IActionResult> createRole(CraeteRoleDto role)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _account.CreateRoleAsync(role);
            if (!response.flag) return BadRequest(response.message);

            return Ok(response.message);

        }

        [HttpGet("identity/role/list")]
        public async Task<IActionResult> GetRoles()
        {


            return Ok(await _account.GetRolesAsync());

        }


        [HttpPost("identity/role/change")]
        public async Task<IActionResult> changeRole(ChangeRoleDto role)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _account.ChangeUserRoleAsync(role);
            if (!response.flag) return BadRequest(response.message);

            return Ok(response.message);

        }
        
    }
}
