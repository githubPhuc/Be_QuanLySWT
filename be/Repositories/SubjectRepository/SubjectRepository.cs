    using be.Models;
using Microsoft.Identity.Client;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace be.Repositories.SubjectRepository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SwtDbContext _context;

        public SubjectRepository()
        {
            _context = new SwtDbContext();
        }

        public async Task<object> GetAllSubject()
        {
            var data = (from subject in _context.Subjects
                        select new
                        {
                            subject.SubjectId,
                            subject.SubjectName,
                            subject.ImgLink,
                        }).ToList();
            return new
              {
                status = 200,
                data,
            };
        }

        public object GetSubjectByTopicType(int topicType)
        {
            int id = topicType;
            var datetime = new DateTime(2023, 8, 18, 9, 6, 0);

            var dateTimeNow = DateTime.Now;
            var check = false;
            if (datetime >= dateTimeNow)
            {
                check = true;
            } else
            {
                check = false;
            }
            var data = (from subject in _context.Subjects
                        join question in _context.Questions
                        on subject.SubjectId equals question.CourseChapterId
                        join topic in _context.Topics
                        on question.TopicId equals topic.TopicId
                        where topic.TopicType == topicType && topic.FinishTestDate >= DateTime.Now
                        select new
                        {
                            topicType = topicType,
                            subject.SubjectId,
                            subject.SubjectName,
                            subject.ImgLink,
                        }).Distinct().ToList();
            return new
            {
                status = 200,
                data,
            };
        }
    }
}
