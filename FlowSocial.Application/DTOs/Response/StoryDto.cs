namespace FlowSocial.Application.DTOs.Response;

public class StoryDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string MediaUrl { get; set; }
    public string Caption { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}