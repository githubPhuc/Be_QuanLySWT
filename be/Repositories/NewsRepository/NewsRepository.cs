using be.DTOs;
using be.Models;
using System.Drawing;

namespace be.Repositories.NewsRepository
{
    public class NewsRepository : INewsRepository
    {
        private readonly SwtDbContext _context;

        public NewsRepository()
        {
            _context = new SwtDbContext();
        }

        public object Addews(NewsDTO newsDTO)
        {
            var news = new News();
            var category = _context.Newcategorys.FirstOrDefault(x => x.CategoryName.Contains(newsDTO.CategoryName));  
            news.NewCategoryId = category.NewCategoryId;
            news.AccountId = newsDTO.AccountId;
            news.Title = newsDTO.Title;
            news.Subtitle = newsDTO.SubTitle;
            news.Image = newsDTO.Image;
            news.Content = newsDTO.Content;
            news.DateCreated = DateTime.Now;
            news.Status = "0";
            _context.News.Add(news);
            _context.SaveChanges();
            return new
            {
                message = "Add News Succfully",
                status = 200,
            };
        }

        public object ChangeStatusNews(int newsId, string status)
        {
            var news = _context.News.FirstOrDefault(x => x.NewId == newsId);
            if(news == null)
            {
                return new
                {
                    message = "Not found to change",
                    status = 400,
                };
            }
            news.Status = status;
            _context.SaveChanges();
            return new
            {
                message = "Change status successfully",
                status = 200,
            };
        }

        public object EditNews(NewsDTO newsDTO)
        {
            var news = _context.News.SingleOrDefault(x => x.NewId == newsDTO.NewsId);   
            if(news == null)
            {
                return new
                {
                    message = "Not found to return",
                    status = 400,
                };
            }
            var editCategory = _context.Newcategorys.FirstOrDefault(x => x.CategoryName.Contains(newsDTO.CategoryName));
            news.NewCategoryId = editCategory.NewCategoryId;
            news.Title = newsDTO.Title;
            news.Subtitle = newsDTO.SubTitle;
            news.Image = newsDTO.Image;
            news.Content = newsDTO.Content;
            _context.SaveChanges();
            return new
            {
                message = "Edit Successfully",
                status = 200,
                data = news,
            };
        }

        public object GetAllNews()
        {
            var result = from news in _context.News
                       select new
                       {
                           newsId = news.NewId,
                           image = news.Image,
                           title = news.Title,
                           subTitle = news.Subtitle,
                           accountName = news.Account.FullName,
                           accountId = news.AccountId,
                           DateCreated = news.DateCreated,
                           status = news.Status,
                           categoryName = news.NewCategory.CategoryName,
                           categoryId = news.NewCategoryId,
                           content = news.Content,
                       };
            var data = result.OrderByDescending(x => x.newsId);
            if(data == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get data successfully",
                status = 200,
                data,
            };
        }

        public object GetAllNewsCategory()
        {
            var data = _context.Newcategorys;
            if(data == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get data successfully",
                status = 200,
                data,
            };
        }

        public object GetAllNewsInUserPage()
        {
            try
            {
                var firstNews = _context.News.Where(n=>n.Status=="1").OrderByDescending(x => x.NewId).FirstOrDefault();
                var firstNewsDTO = new FirstNews();
                firstNewsDTO.NewsId = firstNews.NewId;
                Newcategory category1 = _context.Newcategorys.SingleOrDefault(x => x.NewCategoryId == firstNews.NewCategoryId);
                firstNewsDTO.CategoryName = category1.CategoryName;
                firstNewsDTO.Title = firstNews.Title;
                firstNewsDTO.Image = firstNews.Image;
                firstNewsDTO.CreatedDay = firstNews.DateCreated?.ToString("dd/MM/yyyy");
                var dailyNews = DailyNews();
                var otherNews = OtherNews();
                return new
                {
                    message = "Get data successfully",
                    status = 200,
                    firstNew = firstNewsDTO,
                    dailyNew = dailyNews,
                    otherNew = otherNews,
                };
            }
            catch
            {
                return new
                {
                    message = "Get data failed",
                    status = 400,
                };
            }
        }

        public List<DailyNews> DailyNews()
        {
            var firstNews = _context.News.OrderByDescending(x => x.NewId).FirstOrDefault();
            var newsList = _context.News.Where(x => x.NewId != firstNews.NewId && x.Status=="1").OrderByDescending(x => x.NewId).ToList();
            List<DailyNews> dailyNews = new List<DailyNews>();
            var count = 0;
            foreach (var currentNews in newsList)
            {
                DailyNews news = new DailyNews();
                news.NewsId = currentNews.NewId;
                var id = currentNews.NewCategoryId;
                var category2 = _context.Newcategorys.SingleOrDefault(x => x.NewCategoryId == id);

                if (category2 == null)
                {
                    return null;
                }
                news.CategoryName = category2.CategoryName;
                news.Title = currentNews.Title;
                news.Image = currentNews.Image;
                dailyNews.Add(news);

                count++;
                if (count == 3)
                {
                    break;
                }
            }
            return dailyNews;
        }

        public List<OtherNews> OtherNews()
        {
            var otherNewsList = _context.News.Where(x=>x.Status=="1").Take(3).ToList();
            List<OtherNews> otherNews = new List<OtherNews>();

            foreach (var otherNewsItem in otherNewsList)
            {
                OtherNews other = new OtherNews();
                other.NewsId = otherNewsItem.NewId;
                Newcategory category3 = _context.Newcategorys.SingleOrDefault(x => x.NewCategoryId == otherNewsItem.NewCategoryId);

                if (category3 == null)
                {
                    return null;
                }
                other.CategoryName = category3.CategoryName;
                other.Title = otherNewsItem.Title;
                other.Image = otherNewsItem.Image;
                other.SubTitle = otherNewsItem.Subtitle;
                otherNews.Add(other);
            }
            return otherNews;
        }

        public object GetNewDetail(int newsId)
        {
            var dailyNews = DailyNews();
            try
            {
                var news = _context.News.SingleOrDefault(x => x.NewId == newsId);
                if (news == null)
                {
                    return new
                    {
                        message = "No data to return",
                        status = 400,
                    };
                }

                var detailNews = new NewDetail
                {
                    NewsId = news.NewId,
                    Title = news.Title,
                    Subtitle = news.Subtitle,
                    Content = news.Content,
                    CreateDate = news.DateCreated?.ToString("dd-MM-yyyy"),
                    CategoryName = news.NewCategory?.CategoryName // Kiểm tra null ở đây
                };

                return new
                {
                    message = "Get data successfully",
                    status = 200,
                    data = detailNews,
                    dailyNews = dailyNews,
                };
            }
            catch
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
        }


        public object GetNewsByPage(int page, int pageSize)
        {
            try
            {
                int startIndex = (page - 1) * pageSize;
                var totalNews = GetTotalNewsCount();
                var result = _context.News.Skip(startIndex).Take(pageSize);
                return new
                {
                    message = "Get data succfully",
                    status = 200,
                    totalCount = totalNews,
                    data = result,
                };
            } catch
            {
                return new
                {
                    message = "Failed",
                    status = 400,
                };
            }
        }

        public int GetTotalNewsCount()
        {
            return _context.News.Count();
        }
    }
}
