using be.Repositories.ModRepository;
using be.Repositories.TopicRepository;
using be.DTOs;

namespace be.Services.TopicService
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        public TopicService()
        {
            _topicRepository = new TopicRepository();
        }

        public object ChangeStatusTopic(int topicId, string status)
        {
            return _topicRepository.ChangeStatusTopic(topicId, status); 
        }

        public object CreateTopic(CreateTopic createTopic)
        {
            return _topicRepository.CreateTopic(createTopic);
        }

        public object GetAllTopcOfExam()
        {
            return _topicRepository.GetAllTopcOfExam();
        }

        public object GetAllTopic()
        {
            return _topicRepository.GetAllTopic();
        }

        public async Task<object> GetTopicByGrade(int? grade, int subjectId, int? topicType, int accountId)
        {
            return await _topicRepository.GetTopicByGrade(grade, subjectId, topicType, accountId);

        }

        public async Task<object> GetRankingOfTopic(int topicId, int topicType)
        {
            return await _topicRepository.GetRankingOfTopic(topicId, topicType);
        }

        public object GetTopicByTopicType(int topicType)
        {
            return _topicRepository.GetTopicByTopicType(topicType);
        }

        public object GetTopicById(int topicId)
        {
            return _topicRepository.GetTopicById(topicId);
        }

        public object UpdateTopic(EditTopic editTopic)
        {
            return _topicRepository.EditTopic(editTopic);
        }
    }
}
