using System.ComponentModel.DataAnnotations;

namespace FlowSocial.Domain.Models;

public class Notification
{
    [Key]
    public int Id { get; set; }

    // Foreign Key
    public string RecipientId { get; set; }
    public ApplicationUser? Recipient { get; set; }

    public string? SenderId { get; set; } // Can be null for system notifications
    public ApplicationUser? Sender { get; set; }

    [Required]
    public string Type { get; set; } // e.g., "like", "comment", "follow", "message"

    public int? TargetId { get; set; } // PostId, CommentId, etc. (depends on Type)

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsRead { get; set; } = false;
}