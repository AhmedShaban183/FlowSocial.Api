

using FlowSocial.Application.Common.DTO.Request;
using FlowSocial.Application.DTOs.Response;



namespace FlowSocial.Application.Services.InterfaceService;

public interface IStoryService
{

    Task<StoryDto> GetStoryByIdAsync(int id);
    
  
    Task<IEnumerable<StoryDto>> GetStoriesByUserIdAsync(string userId);

    Task<IEnumerable<StoryDto>> GetStoriesFromFollowingUsersAsync(string userId);

   
    Task<GeneralResponse> CreateStoryAsync(CreateStoryRequest request, string userId);

    
    Task<GeneralResponse> DeleteStoryAsync(int storyId, string userId);
}