using Microsoft.AspNetCore.Http;

namespace FlowSocial.Application.Common.DTO.Request
{
    public class CreatePostDTO
    {
        public IFormFile? Image { get; set; }
        public string Caption { get; set; }
        
    }
}
