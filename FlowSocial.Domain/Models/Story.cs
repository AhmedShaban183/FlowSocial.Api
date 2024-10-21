using System.ComponentModel.DataAnnotations;

namespace FlowSocial.Domain.Models;

public class Story
{
    [Key]
    public int Id { get; set; }

    // Foreign Key
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    [Required]
    public string MediaUrl { get; set; }

    public string Caption {  get; set; }    

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ExpiresAt { get; set; } 
}