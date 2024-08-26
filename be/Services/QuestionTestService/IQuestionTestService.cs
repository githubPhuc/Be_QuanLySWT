namespace be.Services.QuestionTestService
{
    public interface IQuestionTestService
    {
        object AddQuestionTest(int questionId, int testDetailId, int? answerId);
    }
}
