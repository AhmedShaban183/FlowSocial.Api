namespace FlowSocial.Application.DTOs.Response
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; }

        public string User { get; set; }

        public int LikesCount { get; set; }
        public bool? IsLikedByUser { get; set; }

        public List<CommentDTO> Comments { get; set; }= new List<CommentDTO>();
    }
}
