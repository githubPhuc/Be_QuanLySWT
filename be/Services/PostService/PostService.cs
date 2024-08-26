using be.DTOs;
using be.Models;
using be.Repositories.PostRepository;

namespace be.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService()
        {
            _postRepository = new PostRepository();
        }
        public object AddPost(Post post)
        {
            var result = _postRepository.AddPost(post);
            return result;
        }

        public object ChangeStatusPost(int postId, string status)
        {
            return _postRepository.ChangeStatusPost(postId, status);
        }


        public async Task<object> GetAllPost()
        {
            return await _postRepository.GetAllPost();
        }

        public object GetPostById(int postId)
        {
            return _postRepository.GetPostById(postId);
        }

        public object EditPost(EditPostDTO post)
        {
            return _postRepository.EditPost(post);
        }

        public dynamic GetPostByStatus(string? status, int accountId)
        {
            return _postRepository.GetPostByStatus(status, accountId);
        }
        public dynamic GetPostBySubject(int subjectId, int accountId)
        {
            return _postRepository.GetPostBySubject(subjectId, accountId);
        }

        public dynamic GetApprovedPostBySubject(int subjectId)
        {
            return _postRepository.GetApprovedPostBySubject(subjectId);
        }
        public dynamic GetPostBySubjectAndStatus(int subjectId ,string status, int accountId)
        {
            return _postRepository.GetPostBySubjectAndStatus(subjectId, status, accountId);
        }
        public object CountComment(int postId)
        {
            return _postRepository.CountComment(postId);
        }
        public object CountLikedNumberByPost(int postId)
        {
            var result = _postRepository.CountLikedNumberByPost(postId);
            return result;
        }
        public object LikePost(int postId, int accountId)
         {
            var result = _postRepository.LikePost(postId, accountId);
            return result;
        }
        public object UnlikePost(int postId, int accountId)
        {
            var result = _postRepository.UnlikePost(postId, accountId);
            return result;
        }
        public object DeletePost(int postId)
        {
            var result = _postRepository.DeletePost(postId);
            return result;
        }
        public object RejectPost(int postId)
        {
            var result = _postRepository.RejectPost(postId);
            return result;
        }
        public object SavePost(int postId, int accountId)
        {
            var result = _postRepository.SavePost(postId, accountId);
            return result;
        }
        public object UnsavePost(int postId, int accountId)
        {
            var result = _postRepository.UnsavePost(postId, accountId);
            return result;
        }
        public dynamic GetSavedPostByAccountId(int accountId)
        {
            var result = _postRepository.GetSavedPostByAccountId(accountId);
            return result;
        }
    }
}
