


using FlowSocial.Domain.Models;

namespace FlowSocial.Domain.InterfaceRepository
{
    public interface ICommentRepository:IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
    }
}
