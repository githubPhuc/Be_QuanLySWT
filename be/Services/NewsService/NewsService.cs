using be.DTOs;
using be.Repositories.NewsRepository;
using be.Repositories.TopicRepository;

namespace be.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newRepository;
        public NewsService()
        {
            _newRepository = new NewsRepository();
        }

        public object AddNews(NewsDTO newsDTO)
        {
            return _newRepository.Addews(newsDTO);
        }

        public object ChangeStatusNews(int newsId, string status)
        {
            return _newRepository.ChangeStatusNews(newsId, status); 
        }

        public object EditNews(NewsDTO newsDTO)
        {
            return _newRepository.EditNews(newsDTO);
        }

        public object GetAllNews()
        {
            return _newRepository.GetAllNews();
        }

        public object GetAllNewsCategory()
        {
            return _newRepository.GetAllNewsCategory(); 
        }

        public object GetAllNewsInUserPage()
        {
            return _newRepository.GetAllNewsInUserPage();
        }

        public object GetNewDetail(int newsId)
        {
            return _newRepository.GetNewDetail(newsId);
        }

        public object GetNewsByPage(int page, int pageSize)
        {
            return _newRepository.GetNewsByPage(page, pageSize);    
        }

       
    }
}
