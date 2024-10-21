using Microsoft.AspNetCore.Http;

namespace FlowSocial.Application.Common.DTO.Request;

public class CreateStoryRequest
{
    public IFormFile MediaFile { get; set; }
    public string Caption { get; set; } 
}