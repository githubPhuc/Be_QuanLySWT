using be.Models;

namespace be.Repositories.QuestionTestRepository
{
    public class QuestionTestRepository : IQuestionTestRepository
    {
        private readonly SwtDbContext _context;

        public QuestionTestRepository()
        {
            _context = new SwtDbContext();
        }

        public object AddQuestionTest(int questionId, int testDetailId, int? answerId)
        {
            try
            {
                Questiontest questiontest = new Questiontest();
                questiontest.QuestionId = questionId;
                questiontest.TestDetailId = testDetailId;
                questiontest.AnswerId = answerId;
                questiontest.Status = "topic";
                questiontest.DateCreated = DateTime.UtcNow.AddHours(7);
                questiontest.DateUpdated = DateTime.UtcNow.AddHours(7);
                questiontest.DateDelete = DateTime.UtcNow.AddHours(7);
                _context.Add(questiontest);
                _context.SaveChanges();
                return new
                {
                    questiontest,
                    status = 200
                };
            }
            catch
            {
                return new
                {
                    status = 400
                };
            }

        }
    }
}
