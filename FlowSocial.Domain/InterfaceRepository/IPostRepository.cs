
using FlowSocial.Domain.Models;
namespace FlowSocial.Domain.InterfaceRepository
{
    public interface IPostRepository:IGenericRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId);

        Task<Post?> GetByIdAsync(int id);
    }
}
