using be.Repositories.StatictisRepository;
using be.Repositories.SubjectRepository;

namespace be.Services.StatictisService
{
    public class StatictisService : IStatictisService
    {
        private readonly IStatictisRepository _statictisRepository;

        public StatictisService()
        {
            _statictisRepository = new StatictisRepository();
        }

        public object GetAllQuestionDoneByUser(int userId)
        {
            return _statictisRepository.GetAllQuestionDoneByUser(userId);
        }

        public object GetTestDetails()
        {
            return _statictisRepository.GetTestDetails();
        }

        public object StaticsticUser()
        {
            return _statictisRepository.StaticsticUser();
        }

        public object StatictisQuestionBySubject(int? subjectId)
        {
            return _statictisRepository.StatictisQuestionBySubject(subjectId);
        }

        public object StatictisTopicByMonth(int? year)
        {
            return _statictisRepository.StatictisTopicByMonth(year);
        }

        public object StatictisUserByMonth(int? year)
        {
            return _statictisRepository.StatictisUserByMonth(year);
        }

        public object StatictsticQuestion()
        {
            return _statictisRepository.StatictsticQuestion();
        }

        public object StatictsticTopic()
        {
            return _statictisRepository.StatictsticTopic();
        }

        public object StatisticsUserByDay(int? month)
        {
            return _statictisRepository.StatisticsUserByDay(month);
        }
    }
}
