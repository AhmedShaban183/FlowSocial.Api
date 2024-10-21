using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Application.DTOs.Request
{
    public class SendMessageDto
    {
        public string ReceiverId {  get; set; }
        public string Message { get; set; }
    }
}
