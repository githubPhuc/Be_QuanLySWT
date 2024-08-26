using be.Repositories.QuestionTestRepository;
using be.Repositories.TestDetailRepository;

namespace be.Services.QuestionTestService
{
    public class QuestionTestService : IQuestionTestService
    {
        private readonly IQuestionTestRepository _questionTestRepository;

        public QuestionTestService()
        {
            _questionTestRepository = new QuestionTestRepository();
        }

        public object AddQuestionTest(int questionId, int testDetailId, int? answerId)
        {
            var result = _questionTestRepository.AddQuestionTest(questionId, testDetailId, answerId);
            return result;
        }
    }
}
