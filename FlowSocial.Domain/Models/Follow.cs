using System.ComponentModel.DataAnnotations;

namespace FlowSocial.Domain.Models;

public class Follow
{
    [Key]
    public int Id { get; set; }

    // Foreign Keys
    public string FollowerId { get; set; }
    public ApplicationUser Follower { get; set; }

    public string FollowingId { get; set; }
    public ApplicationUser Following { get; set; }
}