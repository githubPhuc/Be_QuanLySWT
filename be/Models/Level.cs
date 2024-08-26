using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Level
{
    public int LevelId { get; set; }

    public string? LevelName { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
