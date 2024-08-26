using be.DTOs;

namespace be.Services.TopicService
{
    public interface ITopicService
    {
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
