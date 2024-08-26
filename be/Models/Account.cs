using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public int? RoleId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? FullName { get; set; }

    public DateTime? BirthDay { get; set; }

    public string? Phone { get; set; }

    public string? SchoolName { get; set; }

    public string? Avatar { get; set; }

    public string? Gender { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<Postcomment> Postcomments { get; set; } = new List<Postcomment>();

    public virtual ICollection<Postfavourite> Postfavourites { get; set; } = new List<Postfavourite>();

    public virtual ICollection<Postlike> Postlikes { get; set; } = new List<Postlike>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Reportpost> Reportposts { get; set; } = new List<Reportpost>();

    public virtual Role? Role { get; set; }
}
