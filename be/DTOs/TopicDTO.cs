namespace be.DTOs
{
    public class TopicDTO
    {
        public int? TopicId { get; set; }
        public string TopicName { get; set; }
        public string? Duration { get; set; }
        public int? TotalQuestion { get; set; }
        public double? Score { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int? TopicType { get; set; }
        public string? TopicTypeName { get; set; }
        public int? GradeId { get; set; }
        public string? Grade { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Status { get; set; }
        public string? StartTestDate { get; set; }
        public string? FinishTestDate { get; set; }
        public DateTime? BeginTestDate { get; set; }
        public DateTime? EndTestDate { get;set; }
    }

    public class CreateTopic
    {
        public string? TopicName { get; set; }
        public string? Duration { get; set; }
        public int? SubjectId { get; set; }
        public int? TopicType { get; set; }
        public int? Grade { get; set; }
        public DateTime? StartTestDate { get; set; }
        public DateTime? FinishTestDate { get; set; }
    }

    public class EditTopic
    {
        public int TopicId { get; set; }    
        public string? TopicName { get; set; }
        public string? Duration { get; set; }
        public int? SubjectId { get; set; }
        public int? TopicType { get; set; }
        public int? Grade { get; set; }
        public DateTime? StartTestDate { get; set; }
        public DateTime? FinishTestDate { get; set; }
    }


}
