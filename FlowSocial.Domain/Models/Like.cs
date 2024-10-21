using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowSocial.Domain.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        // Foreign Keys
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
