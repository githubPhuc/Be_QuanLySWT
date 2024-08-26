using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Combination
{
    public int CombinationId { get; set; }

    public int? SchoolId { get; set; }

    public string? CombinationName { get; set; }

    public string? MajorName { get; set; }

    public double? Score { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual School? School { get; set; }
}
