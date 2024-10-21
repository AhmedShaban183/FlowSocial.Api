
using FlowSocial.Domain.Models;

namespace FlowSocial.Domain.InterfaceRepository
{
    public interface IFolllowRepository:IGenericRepository<Follow>
    {
        Task<Follow?> GetFollowRelationshipAsync(string followerId, string followingId);

        
        Task<bool> IsFollowingAsync(string followerId, string followingId);

        Task<IEnumerable<ApplicationUser>> GetFollowersAsync(string userId);

       
        Task<IEnumerable<ApplicationUser>> GetFollowingAsync(string userId);
       int GetCountFollowers(string userId);
        int GetCountFollowings(string userId);

    }
}
