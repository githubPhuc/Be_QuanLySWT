using be.DTOs;
using be.Models;
using be.Repositories.CouseCharter;
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
        /// <summary>
        /// load chương cho user quản lý
        /// </summary>
        /// <param name="ChapterTitleSearch"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Admin load chương theo id id topic
        /// </summary>
        /// <param name="TopicId"></param>
        /// <returns></returns>
        [HttpGet("GetAllListCouseCharterByTopicId")]
        public async Task<ActionResult> GetAllListCouseCharterByTopicId(int TopicId)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.GetAllListCouseCharterByTopicId(TopicId);
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
        /// <summary>
        /// Load tất cả câu hỏi theo chương quyền admin
        /// </summary>
        /// <param name="IdCouseChapter"></param>
        /// <param name="ChapterTitleSearch"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Load chương theo môn và khối cho sinh viên quyền user
        /// </summary>
        /// <param name="GradeId"></param>
        /// <param name="SubjectId"></param>
        /// <param name="ChapterSearch"></param>
        /// <returns>lst data </returns>
        [HttpGet("GetCouseCharterByGrade")]
        public async Task<ActionResult> GetCouseCharterByGrade(int GradeId, int SubjectId, string? ChapterSearch)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.GetCouseCharterByGrade(GradeId,SubjectId, ChapterSearch);
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
        /// <summary>
        /// Load data câu hỏi cho người dùng 
        /// </summary>
        /// <param name="IdCourseChapter"></param>
        /// <returns></returns>
        [HttpGet("GetQuestionByCourseChaptersInUser")]
        public async Task<ActionResult> GetQuestionByCourseChaptersInUser(int IdCourseChapter)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.GetQuestionByCourseChaptersInUser(IdCourseChapter);
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
        [HttpPost("AddCourceCharter")]
        public async Task<ActionResult> AddCourceCharter(PostDataInsertCourseChapter model)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.AddCourceCharter(model);
                responseAPI.Data = null;
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
        /// <summary>
        /// Admin update chương 
        /// </summary>
        /// <param name="IdCourceChapter"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("UpdateCourceCharter")]
        public async Task<ActionResult> UpdateCourceCharter(int IdCourceChapter, PostDataInsertCourseChapter model)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.UpdateCourceCharter(IdCourceChapter,model);
                responseAPI.Data = null;
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
        /// <summary>
        /// Admin xóa chương 
        /// </summary>
        /// <param name="IdCourceChapter"></param>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        [HttpPost("DeleteCourceCharter")]
        public async Task<ActionResult> DeleteCourceCharter(int IdCourceChapter, int AccountId)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.DeleteCourceCharter(IdCourceChapter, AccountId);
                responseAPI.Data = null;
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
        [HttpPost("ConfirmCourceCharter")]
        public async Task<ActionResult> ConfirmCourceCharter(int IdCourceChapter, int AccountId)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.ConfirmCourceCharter(IdCourceChapter, AccountId);
                responseAPI.Data = null;
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
        [HttpPost("AddQuestionInCourseChapterID")]
        public async Task<ActionResult> AddQuestionInCourseChapterID(PostDataInsertQuestionInCourseChapterID model)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.AddQuestionInCourseChapterID( model);
                responseAPI.Data = null;
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
        [HttpPost("UpdateQuestionInCourseChapterID")]
        public async Task<ActionResult> UpdateQuestionInCourseChapterID(PostDataInsertQuestionInCourseChapterID model, int QuestionId)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.UpdateQuestionInCourseChapterID( model, QuestionId);
                responseAPI.Data = null;
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
        [HttpPost("DeleteQuestionInCourseChapterID")]
        public async Task<ActionResult> DeleteQuestionInCourseChapterID(int QuestionId, int AccountId)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.DeleteQuestionInCourseChapterID(QuestionId, AccountId);
                responseAPI.Data = null;
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

        [HttpPost("AddExcelQuestionInCourseChapterID")]
        public async Task<ActionResult> AddExcelQuestionInCourseChapterID(IFormFile file, int AccountId, int CourseChapterID)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.AddExcelQuestionInCourseChapterID(file, AccountId, CourseChapterID);
                responseAPI.Data = null;
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
        [HttpPost("AddQuestionInCourseChapterByTopic")]
        public async Task<ActionResult> AddQuestionInCourseChapterByTopic(AddQuestionInCourseChapterByTopicModel model)
        {
            ReponserApiService<string> responseAPI = new ReponserApiService<string>();
            try
            {
                var data = await _contextICouseCharterRepository.AddQuestionInCourseChapterByTopic(model);
                responseAPI.Data = null;
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
