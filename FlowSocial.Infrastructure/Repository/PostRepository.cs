using Microsoft.EntityFrameworkCore;
using FlowSocial.Domain.Models;
using FlowSocial.Infrastructure.DataContext;
using FlowSocial.Infrastructure.Repository;
using FlowSocial.Domain.InterfaceRepository;


namespace FlowSocial.infrastructure.Repository
{
    public class PostRepository : GeneralRepository<Post>, IPostRepository
    {
        private readonly FlowSocialContext _context;
        public PostRepository(FlowSocialContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await Context.Posts.Include(p => p.User)
               .Include(p => p.Likes)
               .Include(p => p.Comments)
               .OrderByDescending(p => p.CreatedAt)
               .FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId)
        {

            return await Context.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
