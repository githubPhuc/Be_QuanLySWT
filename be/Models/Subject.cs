﻿using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public string? ImgLink { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual ICollection<Coursechapter> Coursechapters { get; set; } = new List<Coursechapter>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
