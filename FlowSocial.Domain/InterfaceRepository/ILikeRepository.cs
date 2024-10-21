
using FlowSocial.Domain.Models;

namespace FlowSocial.Domain.InterfaceRepository
{
    public interface ILikeRepository:IGenericRepository<Like>
    {
        Task<Like> GetLikeByUserAndPostIdAsync(string userId, int postId);
        Task<int> GetLikeCountForPostAsync(int postId);
    }
}
