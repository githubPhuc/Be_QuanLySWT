using be.Models;

namespace be.Repositories.TestDetailRepository
{
    public interface ITestDetailRepository
    {
        public object GetAllTestDetailByAccountID (int accountID);
        public object GetAllSubject();

        public object StatictisUnderstanding(int accountId, string subjectName);
        object AddTestDetail(int accountId);
        object UpdateTestDetail(int testDetailId);
        public object GetTestDetailByTestDetailId(int testDetailId);
        Task<object> GetQuestionTestByTestDetailId(int testDetailId);
        public object GetUserDoTest();

    }
}
