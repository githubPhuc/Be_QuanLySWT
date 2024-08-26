using be.Models;
using be.Services.QuestionService;
using Microsoft.AspNetCore.Mvc;
using be.DTOs;

namespace be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IConfiguration _configuration;

        public QuestionController(IConfiguration configuration, IQuestionService QuestionService)
        {
            _questionService = QuestionService;
            _configuration = configuration;
        }

        [HttpGet("getQuestionByTopicId")]
        public async Task<ActionResult> GetQuestionByTopicId(int topicId)
        {
            try
            {
                var data = await _questionService.GetQuestionByTopicId(topicId);
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getAllQuestionByTopicId")]
        public async Task<ActionResult> GetAllQuestionByTopicId(int topicId)
        {
            try
            {
                var result = _questionService.GetAllQuestionByTopicId(topicId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("addQuestion")]
        public async Task<ActionResult> AddQuestion (CreateQuestionDTO question)
        {
            try
            {
                var result = _questionService.CreateQuestion(question);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost("changeStatusQuestion")]
        public async Task<ActionResult> ChangeStatusQuestion (int questionId, string status)
        {
            try
            {
                var result = _questionService.ChangeStatusQuestion(questionId, status);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("addQuestionByExcel")]
        public async Task<ActionResult> AddQuestionByExcel([FromBody] CreateQuestionByExcelDTO createQuestion)
        {
            try
            {
                Question question = null;
                DateTime nowDay = DateTime.Now;
                for (int i = 1; i < createQuestion.Records.Count; i++)
                {
                    question = new Question();
                    question.AccountId = createQuestion.AccountId;
                    question.TopicId = createQuestion.TopicId;
                    question.QuestionContext = createQuestion.Records[i][0];
                    question.OptionA = createQuestion.Records[i][1];
                    question.OptionB = createQuestion.Records[i][2];
                    question.OptionC = createQuestion.Records[i][3];
                    question.OptionD = createQuestion.Records[i][4];
                    if (createQuestion.Records[i][5].Contains("1"))
                    {
                        question.AnswerId = 1;
                    } else if (createQuestion.Records[i][5].Contains("2"))
                    {
                        question.AnswerId = 2;
                    } else if (createQuestion.Records[i][5].Contains("3"))
                    {
                        question.AnswerId = 3;
                    } else
                    {
                        question.AnswerId = 4;
                    }
                    question.Solution = createQuestion.Records[i][6];
                    if (createQuestion.Records[i][7].Contains("1"))
                    {
                        question.LevelId = 1;
                    }
                    else if (createQuestion.Records[i][7].Contains("2"))
                    {
                        question.LevelId = 2;
                    }
                    else
                    {
                        question.LevelId = 3;
                    }
                    question.DateCreated = nowDay;
                    question.Status = "0";
                    await Task.Run(() => _questionService.AddQuestionByExcel(question));
                }
                return Ok(new
                {
                    message = "Add Sucessfully",
                    status = 200,
                });
            }
            catch (FormatException ex)
            {
                return BadRequest(new
                {
                    message = "Invalid format in input data",
                    status = 400,
                    error = ex.Message
                });
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest(new
                {
                    message = "Invalid index in input data",
                    status = 400,
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Log the exception here for debugging purposes
                return StatusCode(500, new
                {
                    message = "An error occurred",
                    status = 500,
                    error = ex.Message
                });
            }
        }

        [HttpPost("editQuestion")]
        public async Task<ActionResult> EditQuestion (EditQuestionDTO editQuestion)
        {
            try
            {
                var result = _questionService.EditQuestion(editQuestion);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost("approveAllQuestion")]
        public async Task<ActionResult> ApproveAllQuestion(int topicId)
        {
            try
            {
                var result = _questionService.ApproveAllQuestionOfTopic(topicId);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }
    }
}
