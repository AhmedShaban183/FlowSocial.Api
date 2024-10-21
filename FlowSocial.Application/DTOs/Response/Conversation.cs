using FlowSocial.Application.DTOs.Response.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Application.DTOs.Response
{
    public class Conversation
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public UserDto Sender { get; set; }
        public string ReceiverId { get; set; }
        public UserDto Receiver { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
