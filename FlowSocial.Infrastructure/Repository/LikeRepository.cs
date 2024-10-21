using Microsoft.EntityFrameworkCore;
using FlowSocial.Domain.Models;
using FlowSocial.Infrastructure.DataContext;
using FlowSocial.Infrastructure.Repository;
using FlowSocial.Domain.InterfaceRepository;

namespace FlowSocial.infrastructure.Repository
{
    public class LikeRepository: GeneralRepository<Like>, ILikeRepository
    {
        private readonly FlowSocialContext _context;
        public LikeRepository(FlowSocialContext context) : base(context)
        {

            _context = context;
        }

        public async Task<Like> GetLikeByUserAndPostIdAsync(string userId, int postId)
        {

            return (await _context.Likes
                .FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId))!;
        }

        public async Task<int> GetLikeCountForPostAsync(int postId)
        {
            return await _context.Likes
            .CountAsync(l => l.PostId == postId);
        }
    }
}
