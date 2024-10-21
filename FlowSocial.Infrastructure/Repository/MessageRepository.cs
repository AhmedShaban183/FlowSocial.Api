using FlowSocial.Domain.InterfaceRepository;
using FlowSocial.Domain.Models;
using FlowSocial.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Infrastructure.Repository
{
    public class MessageRepository(FlowSocialContext context) : GeneralRepository<DirectMessage>(context), IMessageRepository
    {
       

        public async Task<IEnumerable<DirectMessage>> GetConversationAsync(string userId1, string userId2, int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;

            return await Context.DirectMessages
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                            (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderByDescending(m => m.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();
        }

        public async Task<IEnumerable<DirectMessage>> GetConversion(string UserID1, string UserID2)
        {
                return await Context.DirectMessages
            .Where(m => (m.SenderId == UserID1 && m.ReceiverId == UserID2) ||
                        (m.SenderId == UserID2 && m.ReceiverId == UserID1))
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
        }
    }
}
