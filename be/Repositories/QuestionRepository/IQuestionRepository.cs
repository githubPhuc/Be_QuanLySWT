﻿using be.Models;
using be.DTOs;

namespace be.Repositories.QuestionRepository
{
    public interface IQuestionRepository
    {
        Task<object> GetQuestionByTopicId(int topicId);
        public void AddQuestionByExcel(Question question);
        public Task<object> GetQuestionByTopicIdInUser(int topicId);
        public object GetAllQuestionByTopicId(int topicId);
        public object CreateQuestion(CreateQuestionDTO questionDTO);
        public object ChangeStatusQuestion(int questionId, string status);
        public object EditQuestion(EditQuestionDTO questionDTO);
        public object ApproveAllQuestionOfTopic(int topicId);
    }
}
