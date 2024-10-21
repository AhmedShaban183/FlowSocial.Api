using FlowSocial.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Domain.InterfaceRepository
{
    public interface IMessageRepository:IGenericRepository<DirectMessage>
    {
        Task<IEnumerable<DirectMessage>> GetConversion(string UserID1, string UserID2);

        Task<IEnumerable<DirectMessage>> GetConversationAsync(string userId1, string userId2, int page, int pageSize);
        
    }
}
