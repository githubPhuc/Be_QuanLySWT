namespace be.DTOs
{
    public class QuestionDTO
    {
    }

    public class CreateQuestionDTO
    {
        public int? SubjectId { get; set; }
        public int? AccountId { get; set; }
        public int? AnswerId { get; set; }
        public int? LevelId { get; set; }
        public int? TopicId { get; set; }
        public string? Image { get; set; }
        public string? QuestionContent { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? Solution { get; set; }
        
    }

    public class EditQuestionDTO
    {
        public int? QuestionId { get; set; } 
        public int? AnswerId { get; set; }
        public int? LevelId { get; set; }
        public string? Image { get; set; }
        public string? QuestionContent { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; } 
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? Solution { get; set; }    
    }

    public class CreateQuestionByExcelDTO
    {
        public int SubjectId { get; set; }
        public int AccountId { get; set; }
        public int TopicId { get; set; }
        public IList<IList<string>> Records {get; set;}
    }

    public class DataQuestion
    {
        public string SubjectName { get; set; }
        public int TotalQuestion { get; set; }
    }

    public class DataQuestionByType
    {
        public string QuestionType { get; set; }
        public int TotalQuestion { get; set; }
    }
}
