using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Questiontest
{
    public int TestId { get; set; }

    public int? TestDetailId { get; set; }

    public int? QuestionId { get; set; }

    public int? AnswerId { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual Question? Question { get; set; }

    public virtual Testdetail? TestDetail { get; set; }
}
