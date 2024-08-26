using be.DTOs;
using be.Models;
using be.Repositories.QuestionRepository;
using be.Repositories.TopicRepository;

namespace be.Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        public QuestionService()
        {
            _questionRepository = new QuestionRepository();
        }

        public void AddQuestionByExcel(Question question)
        {
            _questionRepository.AddQuestionByExcel(question);
        }

        public object ApproveAllQuestionOfTopic(int topicId)
        {
            return _questionRepository.ApproveAllQuestionOfTopic(topicId);
        }

        public object ChangeStatusQuestion(int questionId, string status)
        {
            return _questionRepository.ChangeStatusQuestion(questionId, status);    
        }

        public object CreateQuestion(CreateQuestionDTO questionDTO)
        {
            return _questionRepository.CreateQuestion(questionDTO);
        }

        public object EditQuestion(EditQuestionDTO questionDTO)
        {
            return _questionRepository.EditQuestion(questionDTO);   
        }

        public object GetAllQuestionByTopicId(int topicId)
        {
            return _questionRepository.GetAllQuestionByTopicId(topicId);
        }

        public async Task<object> GetQuestionByTopicId(int topicId)
        {
            return await _questionRepository.GetQuestionByTopicId(topicId);
        }
    }
}
