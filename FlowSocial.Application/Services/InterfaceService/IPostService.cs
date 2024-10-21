using FlowSocial.Application.Common.DTO.Request;
using FlowSocial.Application.DTOs.Response;
using FlowSocial.Domain.Models;


namespace FlowSocial.Application.Services.InterfaceService
{
    public interface IPostService
    {

        Task<int> createPost(CreatePostDTO post, string userID);
        Task updatePost(int id, CreatePostDTO post);

        public  Task<IEnumerable<PostDTO>> GetALLPostAsync(string UserID);



        public Task<PostDTO> GetPostService(int id);


        public  Task<bool> Remove(int id);
        public Task<GeneralResponse> Like(string userID, int PostId);

        public Task<GeneralResponse> UnLike(string userID, int PostId);


        public Task<bool> CreateCommentAsync(int postId, string content, string userId);



        public Task<bool> DeleteCommentAsync(int commentId, string userId);
  
    }


}
