using be.Models;

namespace be.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<object> GetAllSubject();
        public object GetSubjectByTopicType(int topicType);

    }
}
