using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Question
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

    public virtual Account? Account { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual Coursechapter? CourseChapter { get; set; }

    public virtual Level? Level { get; set; }

    public virtual ICollection<Questiontest> Questiontests { get; set; } = new List<Questiontest>();

    public virtual Topic? Topic { get; set; }
}
