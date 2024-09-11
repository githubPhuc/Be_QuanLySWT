using be.DTOs;
using be.Repositories.Graden;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeRepository _contextIGradeRepository;
        public GradeController(IGradeRepository contextICouseCharterRepository)
        {
            this._contextIGradeRepository = contextICouseCharterRepository;
        }
        [HttpGet("GetListGrade")]
        public async Task<ActionResult> GetAllListCouseCharter(string? NameSearch)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextIGradeRepository.GetListGrade(NameSearch);
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
        [HttpPost("CreateGrade")]
        public async Task<ActionResult> CreateGrade(string NameGrade, int AccountId)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextIGradeRepository.CreateGrade(NameGrade, AccountId);
                responseAPI.Count = 1;
                responseAPI.Message = data;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [HttpPost("UpdateGrade")]
        public async Task<ActionResult> UpdateGrade(int GradeId, string NameGrade, int AccountId)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextIGradeRepository.UpdateGrade(GradeId, NameGrade, AccountId);
                responseAPI.Count = 1;
                responseAPI.Message = data;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        [HttpPost("DeleteGrade")]
        public async Task<ActionResult> DeleteGrade(int GradeId, int AccountId)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextIGradeRepository.DeleteGrade(GradeId, AccountId);
                responseAPI.Count = 1;
                responseAPI.Message = data;
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
