using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Postlike
{
    public int PostLikeId { get; set; }

    public int? AccountId { get; set; }

    public int? PostId { get; set; }

    public DateTime? LikeDate { get; set; }

    public string? Status { get; set; }

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
