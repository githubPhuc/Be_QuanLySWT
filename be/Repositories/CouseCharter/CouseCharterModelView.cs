﻿using be.Models;

namespace be.Repositories.CouseCharter
{
    public class getCouseCharter
    {
        public int ChapterId { get; set; }

        public int? SubjecId { get; set; }

        public string? ChapterTitle { get; set; }

        public string? MainContent { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? AccountCreated { get; set; }

        public DateTime? DateUpdate { get; set; }

        public int? AccountUpdate { get; set; }

        public DateTime? DateDelete { get; set; }

        public int? AccountDelete { get; set; }

        public bool? Status { get; set; }

        public int? GradeId { get; set; }

        public Grade? Grade { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();

        public Subject? Subject { get; set; }
    }
    public class getQuestionInCourseChapter
    {
        public int QuestionId { get; set; }
        public int? CourseChapterId { get; set; }
        public int? AccountId { get; set; }
        public int? AnswerId { get; set; }
        public int? LevelId { get; set; }
        public int? TopicId { get; set; }
        public string? Image { get; set; }
        public string? QuestionContext { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? Solution { get; set; }
        public string? Status { get; set; }
        public int? UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserUpdated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool? IsDelete { get; set; }
        public int? UserDelete { get; set; }
        public DateTime? DateDelete { get; set; }
        public Account? Account { get; set; }
        public Answer? Answer { get; set; }
        public Coursechapter? CourseChapter { get; set; }
        public Level? Level { get; set; }
        public Topic? Topic { get; set; }
    }
}
