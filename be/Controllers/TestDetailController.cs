using be.DTOs;
using be.Models;
using be.Services.ModService;
using be.Services.TestDetailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDetailController : ControllerBase
    {
        private readonly ITestDetailService _testDetailService;
        private readonly IConfiguration _configuration;

        public TestDetailController(IConfiguration configuration, ITestDetailService testDetailService)
        {
            _testDetailService = testDetailService;
            _configuration = configuration;
        }

        [HttpPost("addTestDetail")]
        public async Task<ActionResult> AddTestDetail(int accountId)
        {
            try
            {
                var result = _testDetailService.AddTestDetail(accountId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("updateTestDetail")]
        public async Task<ActionResult> UpdateTestDetail(int testdetailId)
        {
            try
            {
                var result = _testDetailService.UpdateTestDetail(testdetailId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getTestDetailByTestDetailId")]
        public async Task<ActionResult> GetTestDetailByTestDetailId(int testdetailId)
        {
            try
            {
                var result = _testDetailService.GetTestDetailByTestDetailId(testdetailId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getQuestionTestByTestDetailId")]
        public async Task<ActionResult> GetQuestionTestByTestDetailId(int testdetailId)
        {
            try
            {
                var data = await _testDetailService.GetQuestionTestByTestDetailId(testdetailId);
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getUserDoTest")]
        public async Task<ActionResult> GetUserDoTest()
        {
            try
            {
                var data = _testDetailService.GetUserDoTest();
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
