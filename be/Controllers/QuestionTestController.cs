using be.Services.QuestionTestService;
using be.Services.TestDetailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTestController : ControllerBase
    {
        private readonly IQuestionTestService _questionTestService;
        private readonly IConfiguration _configuration;

        public QuestionTestController(IConfiguration configuration, IQuestionTestService questionTestService)
        {
            _questionTestService = questionTestService;
            _configuration = configuration;
        }

        [HttpPost("addQuestionTest")]
        public async Task<ActionResult> AddQuestionTest(int questionId, int testDetailId, int? answerId)
        {
            try
            {
                var result = _questionTestService.AddQuestionTest(questionId, testDetailId, answerId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
