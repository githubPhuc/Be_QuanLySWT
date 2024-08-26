﻿using be.Services.SubjectService;
using be.Services.TopicService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using be.DTOs;

namespace be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IConfiguration _configuration;

        public TopicsController(IConfiguration configuration, ITopicService topicService)
        {
            _topicService = topicService;
            _configuration = configuration;
        }

        [HttpGet("getTopicByGrade")]
        public async Task<ActionResult> GetTopicByGrade(int? grade, int subjectId, int? topicType, int accountId)
        {
            try
            {
                var data = await _topicService.GetTopicByGrade(grade, subjectId, topicType, accountId);
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getAllTopicOfExam")]
        public async Task<ActionResult> GetAllTopicOfExam()
        {
            try
            {
                var data = _topicService.GetAllTopcOfExam();
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        #region - Manage Topic

        [HttpGet("getAllTopic")]
        public async Task<ActionResult> GetAllTopic()
        {
            try
            {
                var data = _topicService.GetAllTopic();
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("changeStatusTopic")]
        public async Task<ActionResult> ChangeStatusTopic(int topicId, string status)
        {
            try
            {
                var data = _topicService.ChangeStatusTopic(topicId, status);
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("addTopic")]
        public async Task<ActionResult> AddTopic(CreateTopic createTopic)
        {
            try
            {
                var result = _topicService.CreateTopic(createTopic);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getTopicById")]
        public async Task<ActionResult> GetTopicById (int topicId)
        {
            try
            {
                var result = _topicService.GetTopicById(topicId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("editTopic")]
        public async Task<ActionResult> EditTopic (EditTopic editTopic)
        {
            try
            {
                var result = _topicService.UpdateTopic(editTopic);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }
        #endregion

        [HttpGet("getRankingOfTopic")]
        public async Task<ActionResult> GetRankingOfTopic(int topicId, int topicType)
        {
            try
            {
                var data = await _topicService.GetRankingOfTopic(topicId, topicType);
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getTopicByTopicType")]
        public async Task<ActionResult> GetTopicByTopicType(int topicType)
        {
            try
            {
                var data = _topicService.GetTopicByTopicType(topicType);
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
