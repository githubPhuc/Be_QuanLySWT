﻿using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Groupsubject
{
    public int? CombinationId { get; set; }

    public int? SubjectId { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual Combination? Combination { get; set; }

    public virtual Subject? Subject { get; set; }
}
