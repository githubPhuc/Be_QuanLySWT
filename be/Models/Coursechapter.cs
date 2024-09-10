using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Coursechapter
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
    public bool? IsDelete { get; set; }

    public int? GradeId { get; set; }

    public virtual Grade? Grade { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Subject? Subjec { get; set; }
}
