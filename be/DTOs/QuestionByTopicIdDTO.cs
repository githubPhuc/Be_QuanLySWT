namespace be.DTOs
{
    public class QuestionByTopicIdDTO
    {
        public int questionId { get; set; }
        public int? subjectId { get; set; }
        public string? subjectName { get; set; }

        public int accountId { get; set; }
        public string? accountName { get; set; }

        public int answerId { get; set; }
        public string? answer { get; set; }

        public string level { get; set; }
        public int? levelId { get; set; }
        public string topic { get; set; }
        public int? topicId { get; set; }
        public string image { get; set; }
        public string questionContent { get; set; }

        public string optionA { get; set; }
        public string optionB { get; set; }
        public string optionC { get; set; }
        public string optionD { get; set; }
        public string solution { get; set; }
        public DateTime? createDate { get; set; }

        public string? status { get; set; }
        public string? statusString { get; set; }
    }
}
