using FlowSocial.Application.DTOs.Response;
using FlowSocial.Application.DTOs.Response.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Application.Services.InterfaceService
{
    public interface IFollowService
    {
        Task<GeneralResponse> FollowUserAsync(string followerId, string followingId);
        Task<GeneralResponse> UnfollowUserAsync(string followerId, string followingId);
        Task<bool> IsFollowingAsync(string followerId, string followingId);

        Task<IEnumerable<UserDto>> GetFollowersAsync(string userId);
        Task<IEnumerable<UserDto>> GetFollowingAsync(string userId);
    }
}
