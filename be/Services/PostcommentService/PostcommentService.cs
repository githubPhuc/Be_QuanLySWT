using be.DTOs;
using be.Models;
using be.Repositories.PostcommentRepository;

namespace be.Services.PostcommentService
{
    public class PostcommentService : IPostcommentService
    {
        private readonly IPostcommentRepository _postcommentRepository;

        public PostcommentService()
        {
            _postcommentRepository = new PostcommentRepository();
        }
        public object AddPostcomment(Postcomment postcomment)
        {
            var result = _postcommentRepository.AddPostcomment(postcomment);
            return result;
        }

        public dynamic GetCommentByPost(int postId)
        {
            return _postcommentRepository.GetCommentByPost(postId);
        }

        public object ChangeStatusPostcomment(int postcommentId, string status)
        {
            return _postcommentRepository.ChangeStatusPostcomment(postcommentId, status);
        }

        public object EditComment(EditCommentDTO postcomment)
        {
            return _postcommentRepository.EditComment(postcomment);
        }
        public object DeleteComment(int commentId)
        {
            return _postcommentRepository.DeleteComment(commentId);
        }

    }
}
