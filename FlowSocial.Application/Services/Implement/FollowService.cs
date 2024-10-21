using FlowSocial.Application.DTOs.Response;
using FlowSocial.Application.DTOs.Response.Account;
using FlowSocial.Application.Services.InterfaceService;
using FlowSocial.Domain.InterfaceRepository;
using FlowSocial.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Application.Services.Implement
{
    public class FollowService :IFollowService
    {
        private readonly IAccount _account;
        private readonly IFolllowRepository followRepository;

        public FollowService(IFolllowRepository followRepository, IAccount account)
        {
            this.followRepository = followRepository;
            _account = account;
        }

        public async Task<GeneralResponse> FollowUserAsync(string followerId, string followingId)
        {
      
            var follower = await _account.UserExistsAsync(followerId);
            if (follower == false)
            {
                return new GeneralResponse(false,"Follower not found.");
            }

            var following = await _account.UserExistsAsync(followingId);
            if (following == false)
            {
                return new GeneralResponse(false, "Following not found.");
            }

            var existingFollow = await followRepository.GetFollowRelationshipAsync(followerId, followingId);
            if (existingFollow != null)
            {
                return new GeneralResponse(false, "You are already following this user.");
            }

            var follow = new Follow
            {
                FollowerId = followerId,
                FollowingId = followingId
            };

            //await notificationService.CreateFollowNotificationAsync(follow.FollowerId, follow.FollowingId);

            var fol=await followRepository.AddAsync(follow);
            return new GeneralResponse(true, "You are  following this user Now");
        }


        public async Task<GeneralResponse> UnfollowUserAsync(string followerId, string followingId)
        {
            // Check if users exist 
            var follower = await _account.UserExistsAsync(followerId);
            if (follower == false)
            {
                return new GeneralResponse(false, "Follower not found.");
            }

            var following = await _account.UserExistsAsync(followingId);
            if (following == false)
            {
                return new GeneralResponse(false, "Following not found.");
            }

            var follow = await followRepository.GetFollowRelationshipAsync(followerId, followingId);
            if (follow == null)
            {
                return new GeneralResponse(false, "You are not already following this user.");
            }

            await followRepository.DeleteAsync(follow);
            return new GeneralResponse(true, "You are not following this user Now.");

        }


        public async Task<bool> IsFollowingAsync(string followerId, string followingId)
        {
            return await followRepository.IsFollowingAsync(followerId, followingId);
        }


        public async Task<IEnumerable<UserDto>> GetFollowersAsync(string userId)
        {
            var followers = (await followRepository.GetFollowersAsync(userId)).Select(u=>new UserDto {
                Bio=u.Bio,
                Id=u.Id,
                ProfileImageUrl=u.ProfileImageUrl,
                Username=u.Name,
               
                FollowersCount=followRepository.GetCountFollowers(userId),
                FollowingCount= followRepository.GetCountFollowings(userId)


            });
             
            return followers;
        }


        public async Task<IEnumerable<UserDto>> GetFollowingAsync(string userId)
        {
            var following =  (await followRepository.GetFollowingAsync(userId)).Select( u => new UserDto
            {
                Bio = u.Bio,
                Id = u.Id,
                ProfileImageUrl = u.ProfileImageUrl,
                Username = u.Name,
               
                FollowersCount = followRepository.GetCountFollowers(userId),
                FollowingCount = followRepository.GetCountFollowings(userId)


            });
            return following;
        }
    }
}
