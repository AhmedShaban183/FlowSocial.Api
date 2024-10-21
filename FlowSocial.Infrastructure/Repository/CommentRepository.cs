using FlowSocial.Domain.InterfaceRepository;
using FlowSocial.Domain.Models;
using FlowSocial.Infrastructure.DataContext;
using FlowSocial.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;


namespace FlowSocial.infrastructure.Repository
{
    public class CommentRepository : GeneralRepository<Comment>, ICommentRepository
    {
        private readonly FlowSocialContext _context;
        public CommentRepository(FlowSocialContext context):base(context) {
            
            _context =context;
            }

    
        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await Context.Comments
            .Where(c => c.PostId == postId)
            .Include(c => c.User)
            .OrderBy(c => c.CreatedAt).ToListAsync();
        }

     
    }
    
}
