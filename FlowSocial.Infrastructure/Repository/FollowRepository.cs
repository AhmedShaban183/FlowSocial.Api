using FlowSocial.Domain.Models;
using FlowSocial.Infrastructure.DataContext;
using FlowSocial.Infrastructure.Repository;
using FlowSocial.Domain.InterfaceRepository;
using Microsoft.EntityFrameworkCore;

namespace FlowSocial.infrastructure.Repository
{
    public class FollowRepository(FlowSocialContext context) : GeneralRepository<Follow>(context), IFolllowRepository
    {
        public async Task<Follow?> GetFollowRelationshipAsync(string followerId, string followingId)
        {
            return await Context.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);
        }


        public async Task<bool> IsFollowingAsync(string followerId, string followingId)
        {
            return await Context.Follows
                .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);
        }


        public async Task<IEnumerable<ApplicationUser>> GetFollowersAsync(string userId)
        {
            return await Context.Follows
                .Where(f => f.FollowingId == userId).Include(f=>f.Follower).ThenInclude(u=>u.Followers)
                .Select(f => f.Follower)
                .ToListAsync();
        }

     
        public async Task<IEnumerable<ApplicationUser>> GetFollowingAsync(string userId)
        {
            return await Context.Follows
                .Where(f => f.FollowerId == userId).Include(f => f.Follower).ThenInclude(u => u.Following)
                .Select(f => f.Following)
                .ToListAsync();
        }
        public int GetCountFollowers(string userId)
        {
            return  Context.Follows.Where(f => f.FollowingId == userId).Count();
                
        }
        public int GetCountFollowings(string userId)
        {
            return Context.Follows.Where(f => f.FollowerId == userId).Count();
        }

    }

}
