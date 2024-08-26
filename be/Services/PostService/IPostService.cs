using be.DTOs;
using be.Models;

namespace be.Services.PostService
{
    public interface IPostService
    {
        Task<object> GetAllPost();
        object GetPostById(int PostId);
        object AddPost(Post PostId);
        object ChangeStatusPost(int PostId, string status);
        object EditPost(EditPostDTO post);
        dynamic GetPostByStatus(string? status, int accountId);
        dynamic GetPostBySubject(int subjectId, int accountId);
        dynamic GetApprovedPostBySubject(int subjectId);
        dynamic GetPostBySubjectAndStatus(int subjectId, string status, int accountId);
        object CountComment(int postId);
        object CountLikedNumberByPost(int postId);
        object LikePost(int postId, int accountId);
        object UnlikePost(int postId, int accountId);
        object DeletePost(int postId);
        object RejectPost(int postId);
        object SavePost(int postId, int accountId);
        object UnsavePost(int postId, int accountId);
        dynamic GetSavedPostByAccountId(int accountId);
    }
}
