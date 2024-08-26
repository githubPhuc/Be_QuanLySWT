using be.Models;
using be.DTOs;

namespace be.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<object> GetQuestionByTopicId(int topicId);

        public void AddQuestionByExcel(Question question);
        public object GetAllQuestionByTopicId(int topicId);
        public object CreateQuestion(CreateQuestionDTO questionDTO);
        public object ChangeStatusQuestion(int questionId, string status);
        public object EditQuestion(EditQuestionDTO questionDTO);
        public object ApproveAllQuestionOfTopic(int topicId);


    }
}
