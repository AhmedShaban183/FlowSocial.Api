using FlowSocial.Domain.Models;
using FlowSocial.Infrastructure.DataContext;
using FlowSocial.Infrastructure.Repository;
using FlowSocial.Domain.InterfaceRepository;
using Microsoft.EntityFrameworkCore;

namespace FlowSocial.infrastructure.Repository
{
    public class StoryRepository : GeneralRepository<Story>, IStoryRepository
    {
        private readonly FlowSocialContext _context;
        public StoryRepository(FlowSocialContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Story>> GetStoriesByUserIdAsync(string userId)
        {
            return await Context.Stories
           .Where(s => s.UserId == userId)
           .ToListAsync();
        }

        public async Task<IEnumerable<Story>> GetStoriesFromFollowingUsersAsync(string userId)
        {
            return await Context.Stories
            .Where(s => Context.Follows.Any(f => f.FollowerId == userId && f.FollowingId == s.UserId))
            .Include(s => s.User)
            .ToListAsync();
        }
    }
}
