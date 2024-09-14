using be.Models;

namespace be.Repositories.TestDetailRepository
{
    public interface ITestDetailRepository
    {
        public object GetAllTestDetailByAccountID (int accountID);
        public object GetAllSubject();
        public Task<object> StatictisUnderstanding(int accountId, string subjectName);
        //public object StatictisUnderstanding(int accountId, string subjectName);
        public Task<List<GetHistoryModelView>> GetHistoryByAccount(int accountId, string? subjectName);
        object AddTestDetail(int accountId);
        object UpdateTestDetail(int testDetailId);
        public object GetTestDetailByTestDetailId(int testDetailId);
        Task<object> GetQuestionTestByTestDetailId(int testDetailId);
        public object GetUserDoTest();

    }
}
