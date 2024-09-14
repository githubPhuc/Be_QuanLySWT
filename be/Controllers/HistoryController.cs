using be.Services.ModService;
using be.Services.TestDetailService;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class HistoryController : Controller
    {
        private readonly ITestDetailService _testDetailService;
        private readonly IConfiguration _configuration;

        public HistoryController(IConfiguration configuration, ITestDetailService testService)
        {
            _testDetailService = testService;
            _configuration = configuration;
        }

        [HttpGet("getHistory")]
        public async Task<ActionResult> GetAllTestDetail(int accountId)
        {
            try
            {
                var result = _testDetailService.GetAllTestDetailByAccountID(accountId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getAllSubject")]
        public async Task<ActionResult> GetAllSubject()
        {
            try
            {
                var result = _testDetailService.GetAllSubject();
                return Ok(result);  
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("statistic")]
        public async Task<ActionResult> StatisticUnderStanding(int accountId, string subjectName)
        {
            try
            {
                var result =await _testDetailService.StatictisUnderstanding(accountId, subjectName);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
