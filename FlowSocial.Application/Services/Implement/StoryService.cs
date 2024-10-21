

using FlowSocial.Application.Common.DTO.Request;
using FlowSocial.Application.DTOs.Response;
using FlowSocial.Application.Services.InterfaceService;
using FlowSocial.Domain.InterfaceRepository;
using FlowSocial.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Octagram.Application.Services;

public class StoryService : IStoryService
{
    private readonly IStoryRepository storyRepository;
    private readonly UserManager<ApplicationUser> userRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly string url;
    public StoryService(IStoryRepository storyRepository, UserManager<ApplicationUser> userRepository, IHttpContextAccessor _contextAccessor)
    {
        this.storyRepository = storyRepository;
        this.userRepository = userRepository;
        url = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host;
    }

    public async Task<StoryDto> GetStoryByIdAsync(int id)
    {
        var story = storyRepository.GetByIdAsync((x) => x.Id == id).Select(s => new StoryDto {
            Id=s.Id,
            Caption=s.Caption,
            CreatedAt=s.CreatedAt,
            ExpiresAt=s.ExpiresAt,
            MediaUrl = Path.Combine(url, ("images/" + s.MediaUrl)),
            UserId=s.UserId
            
        }).FirstOrDefault();
        return story;
    }
    

    public async Task<IEnumerable<StoryDto>> GetStoriesByUserIdAsync(string userId)
    {
        var stories = await storyRepository.GetStoriesByUserIdAsync(userId);
        var StoriesDto = new List<StoryDto>();
        foreach (var story in stories)
        {
            StoryDto story1 = new StoryDto()
            {
                UserId =story.UserId,
                Caption =story.Caption,
                CreatedAt=story.CreatedAt,
                ExpiresAt=story.ExpiresAt,
                MediaUrl = Path.Combine(url, ("images/" + story.MediaUrl)),
                Id=story.Id
                
            };
            StoriesDto.Add(story1); 
        }
        return StoriesDto;
    }


    public async Task<IEnumerable<StoryDto>> GetStoriesFromFollowingUsersAsync(string userId)
    {
        var stories = await storyRepository.GetStoriesFromFollowingUsersAsync(userId);
        var StoriesDto = new List<StoryDto>();
        foreach (var story in stories)
        {
            StoryDto story1 = new StoryDto()
            {
                UserId = story.UserId,
                Caption = story.Caption,
                CreatedAt = story.CreatedAt,
                ExpiresAt = story.ExpiresAt,
                MediaUrl = Path.Combine(url, ("images/" + story.MediaUrl)),
                Id = story.Id

            };
            StoriesDto.Add(story1);
        }
        return StoriesDto;
    }


    public async Task<GeneralResponse> CreateStoryAsync(CreateStoryRequest request, string userId)
    {
        var user = await userRepository.FindByIdAsync(userId);
        if (user == null)
        {
            return new GeneralResponse(false,"User not found.");
        }

        // Determine media type and process accordingly (image or video)
        var mediaUrl =await GeTImageUrlAsync(request.MediaFile);
        

        var story = new Story
        {
            UserId = userId,
            MediaUrl = mediaUrl ,
            Caption = request.Caption,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        story=await storyRepository.AddAsync(story);
        return new GeneralResponse(true, $"{story.Id}");
    }


    public async Task<GeneralResponse> DeleteStoryAsync(int storyId, string userId)
    {
        var story =  storyRepository.GetByIdAsync((s)=>s.Id== storyId).FirstOrDefault();
        if (story == null)
        {
            return new GeneralResponse(false,"Story not found.");
        }

        if (story.UserId != userId)
        {
            return new GeneralResponse(false, "You are not authorized to delete this story.");
        }

        await storyRepository.DeleteAsync(story);
        return new GeneralResponse(false, " story deleted successfully");

    }


    private async Task<string?> GeTImageUrlAsync(IFormFile image)
    {
        if (image is not null)
        {
            string UniqueName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filepath = "wwwroot/images/" + UniqueName;
            using (FileStream file = new FileStream(filepath, FileMode.Create))
            {
                await image.CopyToAsync(file);
                file.Close();
            }
            return UniqueName;
        }
        return null;


    }
}