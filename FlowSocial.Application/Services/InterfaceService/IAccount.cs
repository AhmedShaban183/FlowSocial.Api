﻿
using FlowSocial.Application.Common.DTO.Request.Account;
using FlowSocial.Application.DTOs.Request.Account;
using FlowSocial.Application.DTOs.Response;
using FlowSocial.Application.DTOs.Response.Account;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Application.Services.InterfaceService
{
    public interface IAccount
    {
        Task CreateAdmin();
        Task<GeneralResponse> CreateAccountAsync(CreateAccountDTO model);
        Task<LoginResponse> LoginAccountAsync(LoginDTO model);

        Task<GeneralResponse> CreateRoleAsync(CraeteRoleDto model);
        Task<IEnumerable<GetRoleDTO>> GetRolesAsync();

        Task<IEnumerable<GetUserWithRolesDTo>> GetUsersWithRoleAsync();
        Task<GeneralResponse> ChangeUserRoleAsync(ChangeRoleDto model);
        Task<LoginResponse> RefreshTokenAsync(RefreshTockenDto model);
        Task<GeneralResponse> ConfirmEmail(string userID, string Token);
        Task<GeneralResponse> RemoveUser(string id);
        Task<GeneralResponse> LogOut(string userId);
        Task SendEmail(string userId);
        Task<GeneralResponse> ResetPassword(string userId, string token, string password);
         Task ForgetPassword(string userEmail);
        Task<GeneralResponse> ChangePassword(ChangePasswordDTO changePasswordDTO);

        Task<bool> UserExistsAsync(string userId);


    }
}
