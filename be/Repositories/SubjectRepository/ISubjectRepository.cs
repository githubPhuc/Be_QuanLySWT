using be.Models;


namespace be.Repositories.SubjectRepository
{
    public interface ISubjectRepository
    {
        Task<object> GetAllSubject();
        public object GetSubjectByTopicType(int topicType);

    }
}
