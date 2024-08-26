namespace be.DTOs
{
    public class PostcommentDTO
    {
        public int PostCommentId { get; set; }
        public int? AccountId { get; set; }
        public int? PostId { get; set; }
        public string? Content { get; set; }
        public string? FileComment { get; set; }
        public string? Status { get; set; }
        public DateTime? CommentDate { get; set; }
    }
}
