

using FlowSocial.Domain.Models;

namespace FlowSocial.Domain.InterfaceRepository
{
    public interface IStoryRepository:IGenericRepository<Story>
    {
        Task<IEnumerable<Story>> GetStoriesByUserIdAsync(string userId);

        
        Task<IEnumerable<Story>> GetStoriesFromFollowingUsersAsync(string userId);
    }
}
