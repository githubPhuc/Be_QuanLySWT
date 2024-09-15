using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Data.Entity;

namespace be.Repositories.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly SwtDbContext _context;

        public PostRepository()
        {
            _context = new SwtDbContext();
        }
        public object AddPost(Post post)
        {
            try
            {
                _context.Add(post);
                _context.SaveChanges();
                return new
                {
                    message = "Add post successfully",
                    post,
                    status = 200
                };
            }
            catch
            {
                return new
                {
                    message = "Add post fail",
                    status = 400
                };
            }
        }

        public object ChangeStatusPost(int postId, string status)
        {
            var updateStatus = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (updateStatus == null)
            {
                return new
                {
                    message = "The post doesn't exist in database",
                    status = 400
                };
            }
            else
            {
                updateStatus.Status = status;
                _context.SaveChanges();
                return new
                {
                    status = 200,
                    updateStatus,
                    message = "Post Update successfully!"
                };
            }
        }
        public async Task<object> GetAllPost()
        {
            var data = _context.Posts
                .Include(p => p.Subject)
                .Include(p => p.Account)
                .Include(p => p.Postcomments)
                .Where(p => p.Status == "Approved")
                .OrderByDescending(p => p.DateCreated)
                .Select(p =>
              new
              {
                  p.PostId,
                  p.SubjectId,
                  p.Subject.SubjectName,
                  p.AccountId,
                  p.Account.Avatar,
                  p.Account.FullName,
                  p.PostText,
                  p.PostFile,
                  p.Status,
                  p.DateCreated,
                  p.Postlikes,
                  p.Postfavourites,
                  countComment = p.Postcomments.Count(),
                  countLike = p.Postlikes.Count(),

              });
            return data;
        }
        public object GetPostById(int postId)
        {
            var data = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        public object EditPost(EditPostDTO post)
        {
            try
            {
                var editPost = _context.Posts.SingleOrDefault(x => x.PostId == post.PostId);
                if (editPost == null)
                {
                    return new
                    {
                        message = "Post Not Found",
                        status = 200,
                    };
                }
                editPost.PostText = post.PostText;
                editPost.PostFile = post.PostFile;
                editPost.SubjectId = post.SubjectId;
                editPost.Status = "Approved";
                _context.SaveChanges();
                return new
                {
                    message = "Post edited successfully",
                    status = 200,
                    editPost,
                };
            }
            catch
            {
                return new
                {
                    message = "Comment edited failed",
                    status = 400,
                };
            }
        }

        public dynamic GetPostByStatus(string? status, int accountId)
        {
            var checkAccount = _context.Accounts.SingleOrDefault(a => a.AccountId == accountId);
            if (checkAccount.RoleId == 1 || checkAccount.RoleId == 2)
            {
                var posts = _context.Posts
                .Include(p => p.Subject)
                .Include(p => p.Postcomments)
                .Include(p => p.Postlikes)
                .Where(p => p.Status == status)
                .OrderByDescending(p => p.DateCreated)
                .Select(p =>
             new
             {
                 p.PostId,
                 p.Subject.SubjectName,
                 p.Account.FullName,
                 p.Account.Avatar,
                 p.PostText,
                 p.PostFile,
                 p.Status,
                 p.DateCreated,
                 p.Postlikes,
                 p.Postfavourites,
                 countComment = p.Postcomments.Count(),
                 countLike = p.Postlikes.Count()
             });
                return posts;
            }
            else
            {
                var posts = _context.Posts
                .Include(p => p.Subject)
                .Include(p => p.Postcomments)
                .Include(p => p.Postlikes)
                .Where(p => p.Status == status && p.AccountId == accountId)
                .OrderByDescending(p => p.DateCreated)
                .Select(p =>
                    new
                    {
                        p.PostId,
                        p.Subject.SubjectName,
                        p.Account.FullName,
                        p.Account.Avatar,
                        p.PostText,
                        p.PostFile,
                        p.Status,
                        p.DateCreated,
                        p.Postlikes,
                        p.Postfavourites,
                        countComment = p.Postcomments.Count(),
                        countLike = p.Postlikes.Count()
                    }); ;
                return posts;
            }
        }
        public dynamic GetPostBySubject(int subjectId, int accountId)
        {
            var checkAccount = _context.Accounts.SingleOrDefault(a => a.AccountId == accountId);
            if (checkAccount.RoleId == 1 || checkAccount.RoleId == 2)
            {
                var posts = _context.Posts
                      .Include(p => p.Subject)
                      .Include(p => p.Postcomments)
                      .Include(p => p.Postlikes)
                      .Where(p => p.Subject.SubjectId == subjectId)
                      .OrderByDescending(p => p.DateCreated)
                      .Select(p => new
                      {
                          p.PostId,
                          p.Subject.SubjectName,
                          p.Account.FullName,
                          p.Account.Avatar,
                          p.PostText,
                          p.PostFile,
                          p.Status,
                          p.DateCreated,
                          p.Postlikes,
                          p.Postfavourites,
                          countComment = p.Postcomments.Count(),
                          countLike = p.Postlikes.Count()
                      });
                return posts;
            }
            else
            {
                var posts = _context.Posts
                    .Include(p => p.Subject)
                    .Include(p => p.Postcomments)
                    .Include(p => p.Postlikes)
                    .Where(p => p.Subject.SubjectId == subjectId && p.AccountId == accountId)
                    .OrderByDescending(p => p.DateCreated)
                    .Select(p => new
                    {
                        p.PostId,
                        p.Subject.SubjectName,
                        p.Account.FullName,
                        p.Account.Avatar,
                        p.PostText,
                        p.PostFile,
                        p.Status,
                        p.DateCreated,
                        p.Postlikes,
                        p.Postfavourites,
                        countComment = p.Postcomments.Count(),
                        countLike = p.Postlikes.Count()
                    });
                return posts;
            }
        }
        public dynamic GetApprovedPostBySubject(int subjectId)
        {
            {
                var posts = _context.Posts
                    .Include(p => p.Subject)
                    .Include(p => p.Postcomments)
                    .Include(p => p.Postlikes)
                    .Where(p => p.Subject.SubjectId == subjectId && p.Status == "Approved")
                    .OrderByDescending(p => p.DateCreated)
                    .Select(p => new
                    {
                        p.PostId,
                        p.Subject.SubjectName,
                        p.Account.FullName,
                        p.Account.Avatar,
                        p.PostText,
                        p.PostFile,
                        p.Status,
                        p.DateCreated,
                        p.Postlikes,
                        p.Postfavourites,
                        countComment = p.Postcomments.Count(),
                        countLike = p.Postlikes.Count()
                    });

                return posts;
            }
        }
        public dynamic GetPostBySubjectAndStatus(int subjectId, string status, int accountId)
        {
            var _Subjects = _context.Subjects.AsNoTracking();
            var _Postfavourites = _context.Postfavourites.AsNoTracking();
            var _Posts = _context.Posts.AsNoTracking();
            var _Accounts = _context.Accounts.AsNoTracking();
            var _Postcomments = _context.Postcomments.AsNoTracking();
            var _Postlikes = _context.Postlikes.AsNoTracking();
            var posts = (from a in _Postfavourites
                         join b in _Posts on a.PostId equals b.PostId
                         select new
                         {
                             a.PostId,
                             SubjectId = b.SubjectId,
                             SubjectName = (from s in _Subjects
                                            where s.SubjectId == b.SubjectId
                                            select s.SubjectName).FirstOrDefault() ?? "",
                             AccountId = b.AccountId,
                             Avatar = (from acc in _Accounts
                                       where acc.AccountId == b.AccountId
                                       select acc.Avatar).FirstOrDefault() ?? "",
                             FullName = (from acc in _Accounts
                                         where acc.AccountId == b.AccountId
                                         select acc.FullName).FirstOrDefault() ?? "",
                             b.PostText,
                             b.PostFile,
                             a.Status,
                             b.DateCreated,
                             b.Postlikes,
                             b.Postfavourites,
                             countComment = _Postcomments.Count(z => z.PostId == b.PostId),
                             countLike = _Postlikes.Count(z => z.PostId == b.PostId)
                         }).ToList();
            var checkAccount = _context.Accounts.SingleOrDefault(a => a.AccountId == accountId);
            if (checkAccount.RoleId == 1 || checkAccount.RoleId == 2)
            {
                posts = posts
                   .Where(p => p.SubjectId == subjectId && p.Status == status)
                   .OrderByDescending(p => p.DateCreated).ToList();
                return posts;
            }
            else
            {
                posts = posts
               .Where(p => p.SubjectId == subjectId && p.Status == status && p.AccountId == accountId)
               .OrderByDescending(p => p.DateCreated).ToList();
                return posts;
            }

        }
        public object CountComment(int postId)
        {
            try
            {
                var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
                var countComment = _context.Postcomments.Where(x => x.PostId == postId).Count();
                if (post == null)
                {
                    return new
                    {
                        message = "Cannot find this post",
                        status = 200,
                    };
                }
                return new
                {
                    countComment,
                };
            }
            catch
            {
                return new
                {
                    status = 400,
                };
            }
        }

        public object CountLikedNumberByPost(int postId)
        {
            try
            {
                var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
                var countLiked = _context.Postlikes.Where(x => x.PostId == postId).Count();
                if (post == null)
                {
                    return new
                    {
                        message = "Cannot find this post",
                        status = 200,
                    };
                }
                return new
                {
                    status = 200,
                    countLiked,
                };
            }
            catch
            {
                return new
                {
                    status = 400,
                };
            }
        }

        public object LikePost(int postId, int accountId)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (post == null)
            {
                return new
                {
                    message = "The post doesn't exist in the database",
                    status = 400
                };
            }
            else
            {
                var postlike = new Postlike
                {
                    AccountId = accountId,
                    PostId = postId,
                    LikeDate = DateTime.Now
                };
                var checkExist = _context.Postlikes.Any(c => c.PostId == postId && c.AccountId == accountId);
                {
                    if (checkExist)
                    {
                        return new
                        {
                            message = "This account has liked this post before!"
                        };
                    }
                    else {
                        _context.Postlikes.Add(postlike);
                        _context.SaveChanges();

                        return new
                        {
                            status = 200,
                            postlike,
                            message = "Post liked"
                        };
                    }
                }
            }
        }

        public object UnlikePost(int postId, int accountId)
        {
            var postUnlike = _context.Postlikes.SingleOrDefault(x => x.PostId == postId && x.AccountId == accountId);
            if (postUnlike == null)
            {
                return new
                {
                    message = "This account has not liked this post before!",
                    status = 400
                };
            }
            else
            {
                _context.Postlikes.Remove(postUnlike);
                _context.SaveChanges();
                return new
                {
                    status = 200,
                    message = "Unlike post successfully!"
                };
            }
        }

        public object DeletePost(int postId)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (post == null)
            {
                return new
                {
                    message = "The post doesn't exist in database",
                    status = 400
                };
            }
            else
            {
                post.Status = "Deleted";
                _context.SaveChanges();
                return new
                {
                    status = 200,
                    message = "Post deleted successfully!"
                };
            }
        }
        public object RejectPost(int postId)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (post == null || post.Status != "Pending")
            {
                return new
                {
                    message = "The post doesn't exist in database or has been approved",
                    status = 400
                };
            }
            else
            {
                post.Status = "Rejected";
                _context.SaveChanges();
                return new
                {
                    status = 200,
                    message = "The post has been rejected to upload!"
                };
            }
        }
        public object SavePost(int postId, int accountId)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (post == null)
            {
                return new
                {
                    message = "The post doesn't exist in the database",
                    status = 400
                };
            }
            else
            {
                var postSave = new Postfavourite
                {
                    AccountId = accountId,
                    PostId = postId,
                    Status = "Saved"
                };
                var checkExist = _context.Postfavourites.Any(c => c.PostId == postId && c.AccountId == accountId);
                {
                    if (checkExist)
                    {
                        return new
                        {
                            message = "This account has saved this post before!"
                        };
                    }
                    else
                    {
                        _context.Postfavourites.Add(postSave);
                        _context.SaveChanges();

                        return new
                        {
                            status = 200,
                            postSave,
                            message = "Post saved"
                        };
                    }
                }
            }
        }
        public object UnsavePost(int postId, int accountId)
        {
            var postUnsave = _context.Postfavourites.SingleOrDefault(x => x.PostId == postId && x.AccountId == accountId);
            if (postUnsave == null)
            {
                return new
                {
                    message = "This account has not saved this post before!",
                    status = 400
                };
            }
            else
            {
                _context.Postfavourites.Remove(postUnsave);
                _context.SaveChanges();
                return new
                {
                    status = 200,
                    message = "Unsave post successfully!"
                };
            }
        }
        public dynamic GetSavedPostByAccountId(int accountId)
        {
            try
            {
                var _Subjects = _context.Subjects.AsNoTracking();
                var _Postfavourites = _context.Postfavourites.AsNoTracking();
                var _Posts = _context.Posts.AsNoTracking();
                var _Accounts = _context.Accounts.AsNoTracking();
                var _Postcomments = _context.Postcomments.AsNoTracking();
                var _Postlikes = _context.Postlikes.AsNoTracking();
                var posts = (from a in _Postfavourites
                             join b in _Posts on a.PostId equals b.PostId
                             where a.AccountId == accountId && a.Status == "Saved" && b.Status == "Approved"
                             select new
                             {
                                 a.PostId,
                                 SubjectId = b.SubjectId,
                                 SubjectName = (from s in _Subjects
                                                where s.SubjectId == b.SubjectId
                                                select s.SubjectName).FirstOrDefault() ?? "",
                                 AccountId = b.AccountId,
                                 Avatar = (from acc in _Accounts
                                           where acc.AccountId == b.AccountId
                                           select acc.Avatar).FirstOrDefault() ?? "",
                                 FullName = (from acc in _Accounts
                                             where acc.AccountId == b.AccountId
                                             select acc.FullName).FirstOrDefault() ?? "",
                                 b.PostText,
                                 b.PostFile,
                                 b.Status,
                                 b.DateCreated,
                                 b.Postlikes,
                                 b.Postfavourites,
                                 countComment = _Postcomments.Count(z => z.PostId == b.PostId),
                                 countLike = _Postlikes.Count(z => z.PostId == b.PostId)
                             }).ToList();

                return posts;
            }
            catch (Exception ex)
            {
                return new
                {
                    message = "This account has not saved any post before!"
                };
            }
        }
    }
}


