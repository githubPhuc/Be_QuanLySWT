using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Newcategory
{
    public int NewCategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
