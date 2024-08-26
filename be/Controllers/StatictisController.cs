using be.Models;
using be.Services.StatictisService;
using be.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/statictis")]
    [ApiController]
    public class StatictisController : Controller
    {
        private readonly IStatictisService _statictisService;
        private readonly IConfiguration _configuration;

        public StatictisController(IStatictisService statictisService, IConfiguration configuration)
        {
            _statictisService = statictisService;
            _configuration = configuration;
        }

        [HttpGet("getTestDetails")]
        public async Task<ActionResult> GetTestDetails()
        {
            try
            {
                var result = _statictisService.GetTestDetails();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("statictissUser")]
        public async Task<ActionResult> StatictisUser()
        {
            try
            {
                var result = _statictisService.StaticsticUser();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("staticsUserByChartMonth")]
        public async Task<ActionResult> StaticsUserByChartMonth(int? year)
        {
            try
            {
                var result = _statictisService.StatictisUserByMonth(year);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("staticsUserByChartDay")]
        public async Task<ActionResult> StaticsUserByDay(int? month)
        {
            try
            {
                var result = _statictisService.StatisticsUserByDay(month);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("statictissTopic")]
        public async Task<ActionResult> StatictisTopic()
        {
            try
            {
                var result = _statictisService.StatictsticTopic();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("staticsTopicByChartMonth")]
        public async Task<ActionResult> StaticsTopicByChartMonth(int? year)
        {
            try
            {
                var result = _statictisService.StatictisTopicByMonth(year);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("statictissQuestion")]
        public async Task<ActionResult> StatictisQuestion()
        {
            try
            {
                var result = _statictisService.StatictsticQuestion();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("statictissQuestionBySubjectId")]
        public async Task<ActionResult> StatictisQuestionBySubjectId(int? subjectId)
        {
            try
            {
                var result = _statictisService.StatictisQuestionBySubject(subjectId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("statictisProcessingLearning")]
        public async Task<ActionResult> StatictisProcessingLearning(int userId)
        {
            try
            {
                var result = _statictisService.GetAllQuestionDoneByUser(userId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
