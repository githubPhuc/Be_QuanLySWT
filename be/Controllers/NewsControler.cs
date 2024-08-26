using be.DTOs;
using be.Services.ModService;
using be.Services.NewsService;
using be.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsControler : Controller
    {
        private readonly INewsService _newsService;

        public NewsControler(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("getAllNews")]
        public async Task<ActionResult> GetAllNews()
        {
            try
            {
                var result = _newsService.GetAllNews();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getAllNewsCategory")]
        public async Task<ActionResult> GetAllNewsCategory()
        {
            try
            {
                var result = _newsService.GetAllNewsCategory();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("addNews")]
        public async Task<ActionResult> AddNews(NewsDTO newsDTO)
        {
            try
            {
                var result = _newsService.AddNews(newsDTO);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost("editNews")]
        public async Task<ActionResult> EditNews(NewsDTO newsDTO)
        {
            try
            {
                var result = _newsService.EditNews(newsDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();    
            }
        }

        [HttpPost("changeStatusNews")]
        public async Task<ActionResult> ChangeStatusNews(int newsId, string status)
        {
            try
            {
                var result = _newsService.ChangeStatusNews(newsId, status);
                return Ok(result);
            }
            catch
            {
                return BadRequest();    
            }
        }

        [HttpGet("displayNewsInUserPage")]
        public async Task<ActionResult> DisplayNewsInUserPage()
        {
            try
            {
                var result = _newsService.GetAllNewsInUserPage();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("displayNewDetail")]
        public async Task<ActionResult> DisplayNewDetail(int newsId)
        {
            try
            {
                var result = _newsService.GetNewDetail(newsId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getNewsInPage")]
        public async Task<ActionResult> GetNewsInPage(int page = 1, int pageSize = 3)
        {
            try
            {
                var result = _newsService.GetNewsByPage(page, pageSize);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
