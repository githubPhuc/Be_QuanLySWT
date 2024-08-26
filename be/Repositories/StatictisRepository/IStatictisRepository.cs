namespace be.Repositories.StatictisRepository
{
    public interface IStatictisRepository
    {
        #region - Statictis Profile User
        public object GetTestDetails();
        public object GetAllQuestionDoneByUser(int userId); 
        #endregion

        #region - Statictis User
        public object StaticsticUser();
        public object StatictisUserByMonth(int? year);
        public object StatisticsUserByDay(int? month);
        #endregion

        #region - Statictis Topic
        public object StatictsticTopic();
        public object StatictisTopicByMonth(int? year);

        #endregion

        #region - Statictis Topic
        public object StatictsticQuestion();
        public object StatictisQuestionBySubject(int? subjectId);
        #endregion
    }
}
