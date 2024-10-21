using System.ComponentModel.DataAnnotations;

namespace FlowSocial.Domain.Models;

public class DirectMessage
{
    [Key]
    public int Id { get; set; }

    // Foreign Keys
    public string SenderId { get; set; }
    public ApplicationUser Sender { get; set; }

    public string ReceiverId { get; set; }
    public ApplicationUser Receiver { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsRead { get; set; } = false;
}