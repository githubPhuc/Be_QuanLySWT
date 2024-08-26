using System;
using System.Collections.Generic;

namespace be.Models;

public partial class News
{
    public int NewId { get; set; }

    public int? NewCategoryId { get; set; }

    public int? AccountId { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Image { get; set; }

    public string? Content { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Newcategory? NewCategory { get; set; }
}
