namespace FlowSocial.Application.DTOs.Response.Account;

public class UserDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Bio { get; set; }
    public string ProfileImageUrl { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }
    //public bool? IsFollowedByUser { get; set; }
}