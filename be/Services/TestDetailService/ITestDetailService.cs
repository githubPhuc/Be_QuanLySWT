namespace be.Services.TestDetailService
{
    public interface ITestDetailService 
    {
        public object GetAllTestDetailByAccountID(int accountID);
        public object GetAllSubject();
        public object StatictisUnderstanding(int accountId, string subjectName); 
        object AddTestDetail(int accountId);
        object UpdateTestDetail(int testdetailId);
        public object GetTestDetailByTestDetailId(int testDetailId);
        Task<object> GetQuestionTestByTestDetailId(int testDetailId);
        public object GetUserDoTest();
    }
}
