using be.DTOs;
using be.Repositories.TopicRepository;

namespace be.Services.TopicService
{
    public interface ITopicService
    {
        public Task<string> DeleteTopicId(int TopicId, int AccountId);
        public Task<string> ComfirmInTopicId(int TopicId, int AccountId);
        public Task<string> DeleteQuestionInQuestionFromTopic(int QuestionId);
        Task<object> GetTopicByGrade(int? grade, int subjectId, int? topicType, int accountId);
        Task<object> GetRankingOfTopic(int topicId, int topicType);
        public object GetTopicByTopicType(int topicType);
        object GetAllTopcOfExam();
        object GetAllTopic();
        public object ChangeStatusTopic(int topicId, string status);
        public object CreateTopic(CreateTopic createTopic);
        public object GetTopicById(int topicId);

        public object UpdateTopic(EditTopic editTopic);


    }
}
