using be.Repositories.PostRepository;
using be.Repositories.TestDetailRepository;

namespace be.Services.TestDetailService
{
    public class TestDetailService : ITestDetailService
    {
        private readonly ITestDetailRepository _testDetailRepository;

        public TestDetailService()
        {
            _testDetailRepository = new TestDetailRepository();
        }

        public object GetAllSubject()
        {
            return _testDetailRepository.GetAllSubject();
        }

        public object GetAllTestDetailByAccountID(int accountID)
        {
            return _testDetailRepository.GetAllTestDetailByAccountID(accountID);
        }

        public object StatictisUnderstanding(int accountId, string subjectName)
        {
            return _testDetailRepository.StatictisUnderstanding(accountId, subjectName);
        }
        public object AddTestDetail(int accountId)
        {
            var result = _testDetailRepository.AddTestDetail(accountId);
            return result;
        }
        public object UpdateTestDetail(int testdetailId)
        {
            var result = _testDetailRepository.UpdateTestDetail(testdetailId);
            return result;
        }
        public object GetTestDetailByTestDetailId(int testdetailId)
        {
            var result = _testDetailRepository.GetTestDetailByTestDetailId(testdetailId);
            return result;
        }

        public async Task<object> GetQuestionTestByTestDetailId(int testdetailId)
        {
            return await _testDetailRepository.GetQuestionTestByTestDetailId(testdetailId);
        }

        public object GetUserDoTest()
        {
            return _testDetailRepository.GetUserDoTest();
        }
    }
}
