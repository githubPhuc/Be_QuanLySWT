    using be.Models;
using Microsoft.Identity.Client;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using be.Helper;

namespace be.Repositories.SubjectRepository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SwtDbContext _context;
        private readonly Defines _Defines;

        public SubjectRepository()
        {
            _context = new SwtDbContext();
            _Defines = new Defines();
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

            var data = (from a in _context.Topics
                          join b in _context.Subjects on a.SubjectId equals b.SubjectId
                          join c in _context.Questions on a.TopicId equals c.TopicId
                          where a.TopicType == topicType && a.FinishTestDate >= DateTime.Now
                        where a.Status == _Defines.ACTIVE_STRING
                        select new
                          {
                              topicType = topicType,
                              SubjectId= b.SubjectId,
                              SubjectName=b.SubjectName,
                              ImgLink= b.ImgLink,
                          }).Distinct().ToList();

                          
            
            return new
            {
                status = 200,
                data,
            };
        }
    }
}
