using be.Models;

namespace be.Repositories.QuestionTestRepository
{
    public interface IQuestionTestRepository
    {
        object AddQuestionTest(int questionId, int testDetailId, int? answerId);
    }
}
