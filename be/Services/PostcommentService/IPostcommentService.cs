using be.DTOs;
using be.Models;

namespace be.Services.PostcommentService
{
    public interface IPostcommentService
    {
        object AddPostcomment(Postcomment postcomment);
        dynamic GetCommentByPost(int postId);
        object ChangeStatusPostcomment(int postCommentId, string status);
        object EditComment(EditCommentDTO postcomment);
        object DeleteComment(int commentId);
    }
}
