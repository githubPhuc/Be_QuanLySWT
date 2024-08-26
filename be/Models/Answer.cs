using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public string? AnswerName { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Questiontest> Questiontests { get; set; } = new List<Questiontest>();
}
