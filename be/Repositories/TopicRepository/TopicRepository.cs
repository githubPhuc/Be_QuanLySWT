using be.DTOs;
using be.Models;
using System.ComponentModel;
using System.Data.Entity.Core.Mapping;
using System.Diagnostics;
using be.DTOs;
using Microsoft.EntityFrameworkCore;

namespace be.Repositories.TopicRepository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly SwtDbContext _context;

        public TopicRepository()
        {
            _context = new SwtDbContext();
        }

        public object ChangeStatusTopic(int topicId, string status)
        {
            var topic = _context.Topics.SingleOrDefault(x => x.TopicId == topicId);
            if (topic == null)
            {
                return new
                {
                    message = "No data to found",
                    status = 400,
                };
            }
            topic.Status = status;
            _context.SaveChanges();
            return new
            {
                message = "Change status successfully",
                status = 200,
                data = topic,
            };
        }

        public object CreateTopic(CreateTopic createTopic)
        {
            try
            {
                var topic = new Topic();
                topic.TopicName = createTopic.TopicName;
                topic.Grade = createTopic.Grade;
                topic.SubjectId = createTopic.SubjectId;
                if(createTopic.Duration != null || createTopic.Duration != "null")
                {
                    topic.Duration = createTopic.Duration;
                }
                topic.TopicType = createTopic.TopicType;
                topic.DateCreated = DateTime.Now;
                topic.Status = "0";
                topic.StartTestDate =  createTopic.StartTestDate;
                topic.FinishTestDate = createTopic.FinishTestDate;
                _context.Topics.Add(topic);
                _context.SaveChanges();
                return new
                {
                    message = "Add successfully",
                    status = 200,
                    data = topic,
                };
            } catch
            {
                return new
                {
                    message = "Add failly",
                    status = 400,
                };
            }
            

        }

        public object EditTopic(EditTopic editTopic)
        {
            var topic = _context.Topics.SingleOrDefault(x => x.TopicId == editTopic.TopicId);
            if(topic == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            topic.TopicName = editTopic.TopicName;
            if(editTopic.TopicType == 5 || editTopic.TopicType == 6)
            {
                topic.Grade = null;
            } else
            {
                topic.Grade = editTopic.Grade;
            }
            topic.SubjectId = editTopic.SubjectId;
            topic.TopicType = editTopic.TopicType;
            if(editTopic.TopicType == 1)
            {
                topic.Duration = null;
                topic.StartTestDate = null;
                topic.FinishTestDate = null;
            } else if (editTopic.TopicType == 6)
            {
                topic.Duration = editTopic.Duration;
                topic.StartTestDate = editTopic.StartTestDate;
                topic.FinishTestDate = editTopic.FinishTestDate;
            } else
            {
                topic.Duration = editTopic.Duration;
                topic.StartTestDate = null;
                topic.FinishTestDate = null;
            }
           
            try
            {
                _context.SaveChanges();
                return new
                {
                    message = "Update successfully",
                    status = 200,
                    data = topic
                };
            } catch
            {
                return new
                {
                    message = "Update failly",
                    status = 400,
                };
            }
        
        }

        public object GetAllTopcOfExam()
        {
            var data = _context.Topics.Where(x => x.TopicType != 1);
            if(data == null)
            {
                return new
                {
                    message = "No Data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get Data Successfully",
                status = 200,
                data,
            };
        }

        public object GetAllTopic()
        {
            var subjectList = _context.Subjects.ToList();
            var GradesList = _context.Grades.ToList();
            

            List < TopicDTO > topicList = new List<TopicDTO>();
            foreach (var item in _context.Topics)
            {
                TopicDTO topicDTO = new TopicDTO();
                topicDTO.TopicId = item.TopicId;
                var subject = subjectList.SingleOrDefault(x => x.SubjectId == item.SubjectId);
                topicDTO.SubjectId = subject.SubjectId;
                if(item.Grade != null)
                {
                    var graden = GradesList.Where(a => a.GradeId == item.Grade).FirstOrDefault();
                    topicDTO.Grade = graden?.NameGrade ?? "";
                    topicDTO.GradeId = graden?.GradeId ?? 0;
                }
                else
                {
                    topicDTO.Grade = "";
                    topicDTO.GradeId = 0;
                }
                topicDTO.SubjectName = subject.SubjectName;
                topicDTO.TopicName = item.TopicName;
                topicDTO.Duration = item.Duration;
                topicDTO.TotalQuestion = item.TotalQuestion;
                topicDTO.TopicType = item.TopicType;
                if (topicDTO.TopicType == 1)
                {
                    topicDTO.TopicTypeName = "Học";
                }
                else if (topicDTO.TopicType == 2)
                {
                    topicDTO.TopicTypeName = "15p";
                }
                else if (topicDTO.TopicType == 3)
                {
                    topicDTO.TopicTypeName = "1 tiết";
                }
                else if (topicDTO.TopicType == 4)
                {
                    topicDTO.TopicTypeName = "Học kì";
                } 
                else if (topicDTO.TopicType == 5)
                {
                    topicDTO.TopicTypeName = "THPT Quốc Gia";

                } else
                {
                    topicDTO.TopicTypeName = "Cuộc thi chung";
                }
                topicDTO.CreateDate = item.DateCreated;
                if (item.Status == "0")
                {
                    topicDTO.Status = "Chờ duyệt";
                }
                else if (item.Status == "1")
                {
                    topicDTO.Status = "Đã duyệt";
                } else
                {
                    topicDTO.Status = "Khóa";
                }
                topicDTO.BeginTestDate = item.StartTestDate;
                topicDTO.EndTestDate = item.FinishTestDate;
                topicList.Add(topicDTO);
            }
            var data = topicList.OrderByDescending(x => x.TopicId);
            if (data == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get data successfully",
                status = 200,
                data,
            };
        }

        public async Task<object> GetTopicByGrade(int? grade, int subjectId, int? topicType, int accountId)
        {
            var listTopic = (from topic in _context.Topics
                             join question in _context.Questions
                             on topic.TopicId equals question.TopicId
                             join subject in _context.Subjects
                             on question.CourseChapterId equals subject.SubjectId
                             where topic.Grade == grade && subject.SubjectId == subjectId && topic.TopicType == topicType && topic.Status == "1" && question.Status == "1"
                             select new
                             {
                                 topic.TopicId,
                                 subject.SubjectId,
                                 topic.TopicName,
                                 subject.SubjectName,
                                 topic.Duration,
                                 topic.TotalQuestion,
                                 topic.TopicType,
                                 topic.Grade,
                                 topic.DateCreated,
                                 topic.Status,
                                 topic.StartTestDate,
                                 topic.FinishTestDate
                             }).Distinct().ToList();
            if (topicType == 1)
            {
                listTopic = listTopic.Where(x => x.TopicType == 1).Where(y => y.Grade == grade).ToList();
            }
            else if (topicType == null)
            {
                listTopic = listTopic.Where(x => x.TopicType != 1).Where(y => y.Grade == grade).ToList();
            }
            else if (grade == null)
            {
                listTopic = listTopic.Where(x => x.TopicType == topicType).ToList();
            }
            else
            {
                listTopic = listTopic.Where(x => x.TopicType == topicType).Where(y => y.Grade == grade).ToList();
            }
            var listTopicSubmited = (from topic in _context.Topics
                                     join question in _context.Questions
                                     on topic.TopicId equals question.TopicId
                                     join questionTest in _context.Questiontests
                                     on question.QuestionId equals questionTest.QuestionId
                                     join testDetail in _context.Testdetails
                                     on questionTest.TestDetailId equals testDetail.TestDetailId
                                     join account in _context.Accounts
                                     on testDetail.AccountId equals account.AccountId
                                     join subject in _context.Subjects
                                     on question.CourseChapterId equals subject.SubjectId
                                     where account.AccountId == accountId && testDetail.Submitted == true
                                     select new
                                     {
                                         topic.TopicId,
                                         account.AccountId,
                                         testDetail.DateCreated,
                                         testDetail.Score,
                                     }).OrderBy(x => x.Score).ToList();

            var data = new List<TopicDTO>();

            foreach (var item in listTopic)
            {
                var topicDTO = new TopicDTO();
                topicDTO.TopicId = item.TopicId;
                topicDTO.TopicName = item.TopicName;
                var totalQuestion = (from topic in _context.Topics
                                     join question in _context.Questions
                                     on topic.TopicId equals question.TopicId
                                     where topic.TopicId == item.TopicId && question.Status == "1"
                                     select new
                                     {
                                         topic.TopicId
                                     }).Count();
                topicDTO.TotalQuestion = totalQuestion;
                topicDTO.Duration = item.Duration;
                topicDTO.StartTestDate = item.StartTestDate?.ToString("dd/MM/yyyy H:mm");
                topicDTO.FinishTestDate = item.FinishTestDate?.ToString("dd/MM/yyyy H:mm");

                foreach (var itemSubmited in listTopicSubmited)
                {
                    if (item.TopicId == itemSubmited.TopicId)
                    {
                        topicDTO.Score = itemSubmited.Score;
                    }
                }
                if (item.FinishTestDate >= DateTime.Now || string.IsNullOrEmpty(topicDTO.FinishTestDate))
                {
                    data.Add(topicDTO);
                }
            }

            return new
            {
                status = 200,
                data,
            };
        }
        
        public object GetTopicById(int topicId)
        {
            var result = from topic in _context.Topics where topic.TopicId == topicId
                         select new
                         {
                             topicId = topic.TopicId,
                             topicName = topic.TopicName,
                             subjectId = topic.SubjectId
                         };
            if(result == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get data sucessfully",
                status = 200,
                data = result.FirstOrDefault(),
            };
        }
        public async Task<object> GetRankingOfTopic(int topicId, int topicType)
        {
            var data = (from topic in _context.Topics
                        join question in _context.Questions
                        on topic.TopicId equals question.TopicId
                        join questionTest in _context.Questiontests
                        on question.QuestionId equals questionTest.QuestionId
                        join testDetail in _context.Testdetails
                        on questionTest.TestDetailId equals testDetail.TestDetailId
                        join account in _context.Accounts
                        on testDetail.AccountId equals account.AccountId
                        join subject in _context.Subjects
                        on question.CourseChapterId equals subject.SubjectId
                        where topic.TopicId == topicId && topic.TopicType == topicType
                        select new
                        {
                            topic.TopicId,
                            topic.TopicName,
                            testDetail.TestDetailId,
                            subject.SubjectName,
                            account.FullName,
                            account.AccountId,
                            testDetail.DateCreated,
                            dateSubmit = String.Format("{0:dd/MM/yyy HH:mm:ss}", testDetail.DateCreated),
                            testDetail.Score,
                        }).OrderBy(x => x.DateCreated).OrderByDescending(x => x.Score).Distinct().ToList();
            return new
            {
                status = 200,
                data,
            };
        }

        public object GetTopicByTopicType(int topicType)
        {
            var data = (from topic in _context.Topics
                        where topic.TopicType == topicType && topic.FinishTestDate >= DateTime.Now
                        select new
                        {
                            topic.TopicId,
                            topic.TopicName,
                            topic.StartTestDate,
                        }).OrderByDescending(x => x.StartTestDate).ToList();
            return new
            {
                status = 200,
                data,
            };
        }
    }
}
