
using Microsoft.EntityFrameworkCore;
using FlowSocial.Application.DTOs.Response;
using FlowSocial.Application.Services.InterfaceService;
using FlowSocial.Domain.InterfaceRepository;
using FlowSocial.Domain.Models;
using FlowSocial.Application.Common.DTO.Request;
using Microsoft.AspNetCore.Http;
using System;

namespace FlowSocial.Application.Services.Implement
{
    public class PostService:IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string url;

        public PostService(IPostRepository postRepository, ILikeRepository likeRepository, ICommentRepository commentRepository, IHttpContextAccessor contextAccessor)
        {
            _postRepository = postRepository;
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;
            _contextAccessor= contextAccessor;
            url = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host;
        }
        public async Task<int> createPost(CreatePostDTO post,string userID)
        {
            Post newpost = new Post() { Caption = post.Caption, ImageUrl = await GeTImageUrlAsync(post.Image), UserId = userID };

          return( await _postRepository.AddAsync(newpost)).Id;


        }
        public async Task updatePost(int id, CreatePostDTO post)
        {

            var old = await _postRepository.GetByIdAsync(id);
            if (old != null)
            {
                old.Caption = post.Caption;
                var oldImageUrl = old.ImageUrl;
                if (post.Image != null)
                {
                    old.ImageUrl = await GeTImageUrlAsync(post.Image);
                    if(oldImageUrl != null)
                    {
                        //delete for old 
                        if(File.Exists(("wwwroot/images/" + oldImageUrl)))
                        {

                        File.Delete(("wwwroot/images/" + oldImageUrl));
                        }
                    }
                }
                
                await _postRepository.UpdateAsync();
                
            }
        }

        public async Task<IEnumerable<PostDTO>> GetALLPostAsync(string UserID)
        {

            var posts = await _postRepository.GetAllAsync().Include(p => p.Likes).Include(p => p.Comments).Include(p => p.User)
                .ToListAsync();

            List<PostDTO> postsDTO = new List<PostDTO>();
            foreach (var post in posts)
            {
                PostDTO postDTO = new PostDTO();
                postDTO.Id = post.Id;
                postDTO.ImageUrl = Path.Combine(url, ("images/" + post.ImageUrl));
                postDTO.Caption = post.Caption;
                postDTO.CreatedAt = post.CreatedAt;
                postDTO.User = post.User.Name;
                foreach (var co in post.Comments)
                {
                    CommentDTO commentDTO = new CommentDTO();
                    commentDTO.Content = co.Content;
                    commentDTO.user = co.User.Name;
                    commentDTO.PostId= post.Id;
                    postDTO.Comments.Add(commentDTO);
                }
                Like l = await _likeRepository.GetLikeByUserAndPostIdAsync(UserID, post.Id);
                if (l != null)
                {
                    postDTO.IsLikedByUser = true;
                }
                else { postDTO.IsLikedByUser = false; }
                postDTO.LikesCount = await _likeRepository.GetLikeCountForPostAsync(post.Id);
                postsDTO.Add(postDTO);

            }
            return postsDTO;
        }

        public async Task<PostDTO> GetPostService(int id)
        {
            var post= await _postRepository.GetByIdAsync(id);
            var PostDto = new PostDTO()
            {
                Id = post.Id,
                Caption = post.Caption,
                CreatedAt = post.CreatedAt,
                ImageUrl = Path.Combine(url, ("images/"+ post.ImageUrl)),
                User = post.User.UserName
            };
            foreach (var co in post.Comments)
            {
                CommentDTO commentDTO = new CommentDTO();
                commentDTO.Content = co.Content;
                commentDTO.user = co.User.Name;
                commentDTO.PostId = post.Id;
                PostDto.Comments.Add(commentDTO);
            }
            Like l = await _likeRepository.GetLikeByUserAndPostIdAsync(post.UserId, post.Id);
            if (l != null)
            {
                PostDto.IsLikedByUser = true;
            }
            else { PostDto.IsLikedByUser = false; }
            PostDto.LikesCount = await _likeRepository.GetLikeCountForPostAsync(post.Id);


            return PostDto;

        }
        public async Task<bool> Remove(int id)
        {
            Post OldDept = await _postRepository.GetByIdAsync(id);
            if (OldDept != null)
            {
                await _postRepository.DeleteAsync(OldDept);
                return true;
            }
            return false;
        }

        public async Task<GeneralResponse> Like(string userID,int PostId)
        {
            var existingLike = await _likeRepository.GetLikeByUserAndPostIdAsync(userID, PostId);
            if (existingLike != null)
            {
                return new GeneralResponse(false, "You have already been liked on the post.");
               
            }

            var like = new Like
            {
                PostId = PostId,
                UserId = userID
            };

          //  await notificationService.CreateLikeNotificationAsync(postId, userId);

            await _likeRepository.AddAsync(like);
            return new GeneralResponse(true, "Liked");  
        }
        public async Task<GeneralResponse> UnLike(string userID, int PostId)
        {


            var existingLike = await _likeRepository.GetLikeByUserAndPostIdAsync(userID, PostId);
            if (existingLike == null)
                return new GeneralResponse(false, "You are not liked on the post.");
  
            await _likeRepository.DeleteAsync(existingLike);
            return new GeneralResponse(true, "Unlike");
        }

        public async Task<bool> CreateCommentAsync(int postId, string content, string userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                return false;
            }

            var comment = new Comment
            {
                Content = content,
                PostId = postId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            //await notificationService.CreateCommentNotificationAsync(comment.Id, userId);

            await _commentRepository.AddAsync(comment);
            return true;
            
        }

       
        public async Task<bool> DeleteCommentAsync(int commentId, string userId)
        {
            var comment =  _commentRepository.GetByIdAsync((x)=>x.Id==commentId).FirstOrDefault();
            if (comment == null)
            {
                return false;
            }

            if (comment.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this comment.");
            }

            await _commentRepository.DeleteAsync(comment);
            return true;
        }
        private async Task<string?> GeTImageUrlAsync(IFormFile image)
        {
            if(image is not null)
            {
                string UniqueName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filepath = "wwwroot/images/" + UniqueName;
                using (FileStream file = new FileStream(filepath, FileMode.Create))
                {
                    await image.CopyToAsync(file);
                    file.Close();
                }
                return UniqueName;
            }
            return null;


        }
    }
}
