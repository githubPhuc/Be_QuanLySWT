using be.DTOs;

namespace be.Services.NewsService
{
    public interface INewsService
    {
        public object GetAllNews();
        public object GetAllNewsCategory();
        public object AddNews(NewsDTO newsDTO);
        public object EditNews(NewsDTO newsDTO);

        public object ChangeStatusNews(int newsId, string status);
        public object GetAllNewsInUserPage();
        public object GetNewDetail(int newsId);
        public object GetNewsByPage(int page, int pageSize);


    }
}
