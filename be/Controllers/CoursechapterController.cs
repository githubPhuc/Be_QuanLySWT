using be.DTOs;
using be.Repositories.ModRepository;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursechapterController : ControllerBase
    {
        private readonly ICouseCharterRepository _contextICouseCharterRepository;
        public CoursechapterController(ICouseCharterRepository contextICouseCharterRepository)
        {
            this._contextICouseCharterRepository = contextICouseCharterRepository;
        }
        [HttpGet("GetAllListCouseCharter")]
        public async Task<ActionResult> GetAllListCouseCharter(string? ChapterTitleSearch)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.GetAllListCouseCharter(ChapterTitleSearch);
                responseAPI.Data = data;
                responseAPI.Count = data.Count();
                responseAPI.Message = "Load thành công!!";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [HttpGet("GetAllListQuestionInCouseCharter")]
        public async Task<ActionResult> GetAllListQuestionInCouseCharter(int IdCouseChapter,string? ChapterTitleSearch)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.GetAllListQuestionInCouseCharter(IdCouseChapter,ChapterTitleSearch);
                responseAPI.Data = data;
                responseAPI.Count = data.Count();
                responseAPI.Message = "Load thành công!!";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
    }
}
