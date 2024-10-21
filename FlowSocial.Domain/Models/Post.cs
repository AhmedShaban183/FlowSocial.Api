using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowSocial.Domain.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        
        public string? ImageUrl { get; set; }
        public string Caption { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        // Navigation Properties
        public List<Like>? Likes { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<PostHashtag> PostHashtags { get; set; }
    }

}

