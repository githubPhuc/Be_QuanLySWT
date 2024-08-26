using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Postcomment
{
    public int PostCommentId { get; set; }

    public int? AccountId { get; set; }

    public int? PostId { get; set; }

    public string? Content { get; set; }

    public string? FileComment { get; set; }

    public string? Status { get; set; }

    public DateTime? CommentDate { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Post? Post { get; set; }
}
