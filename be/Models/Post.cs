using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Post
{
    public int PostId { get; set; }

    public int? SubjectId { get; set; }

    public int? AccountId { get; set; }

    public string? PostText { get; set; }

    public string? PostFile { get; set; }

    public string? Status { get; set; }

    public int? UserCreated { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? UserUpdated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDelete { get; set; }

    public int? UserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Postcomment> Postcomments { get; set; } = new List<Postcomment>();

    public virtual ICollection<Postfavourite> Postfavourites { get; set; } = new List<Postfavourite>();

    public virtual ICollection<Postlike> Postlikes { get; set; } = new List<Postlike>();

    public virtual ICollection<Reportpost> Reportposts { get; set; } = new List<Reportpost>();

    public virtual Subject? Subject { get; set; }
}
