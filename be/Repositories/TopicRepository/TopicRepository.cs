using be.DTOs;
using be.Models;
using Microsoft.EntityFrameworkCore;
using be.Helper;
using be.Repositories.CouseCharter;

namespace be.Repositories.TopicRepository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly SwtDbContext _context;
        private readonly Defines _Defines;

        public TopicRepository()
        {
            _context = new SwtDbContext();
            _Defines = new Defines();
        }

        public object ChangeStatusTopic(int topicId, string status)
        {
            var topic = _context.Topics.SingleOrDefault(x => x.TopicId == topicId && x.IsDelete == false);
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
                if (createTopic.SubjectId > 0)
                {
                    if (!string.IsNullOrEmpty(createTopic.TopicName))
                    {
                        var data = _context.Topics.FirstOrDefault(a => a.TopicName == createTopic.TopicName &&a.Grade == createTopic.Grade);
                        if(data != null)
                        {
                            return new
                            {
                                message = "Topic name already exists",
                                status = 400,
                            };
                        }
                        var topic = new Topic();
                        topic.TopicName = createTopic.TopicName;
                        topic.Grade = createTopic.Grade;
                        topic.SubjectId = createTopic.SubjectId;
                        if(createTopic.Duration != null || createTopic.Duration != "null")
                        {
                            topic.Duration = createTopic.Duration;
                        }
                        topic.TotalQuestion = 0;
                        topic.TopicType = createTopic.TopicType;
                        topic.Status = _Defines.INACTIVE_STRING;
                        topic.StartTestDate =  createTopic.StartTestDate;
                        topic.FinishTestDate = createTopic.FinishTestDate;
                        topic.IsDelete = false;
                        topic.DateCreated = DateTime.Now;
                        topic.DateUpdated = DateTime.Now;
                        topic.DateDelete = DateTime.Now;
                        _context.Topics.Add(topic);
                        _context.SaveChanges();
                        return new
                        {
                            message = "Add successfully",
                            status = 200,
                            data = topic,
                        };

                    }
                    else
                    {
                        return new
                        {
                            message = "Topic Name null",
                            status = 400,
                        };
                    }
                }
                else
                {
                    return new
                    {
                        message = "Subject null",
                        status = 400,
                    };
                }
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
            var topic = _context.Topics.SingleOrDefault(x => x.TopicId == editTopic.TopicId && x.IsDelete == false);
            if(topic == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            topic.TopicName = editTopic.TopicName;
            topic.DateUpdated = DateTime.Now;
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
            var data = _context.Topics.Where(x => x.TopicType != 1&& x.IsDelete ==false);
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
            var subjectList = _context.Subjects.Where(a=>a.IsDelete == false).ToList();
            var GradesList = _context.Grades.ToList();
            

            List < TopicDTO > topicList = new List<TopicDTO>();
            foreach (var item in _context.Topics.Where(a=>a.IsDelete== false))
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
                if (item.Status == _Defines.INACTIVE_STRING)
                {
                    topicDTO.Status = "Chờ duyệt";
                }
                else if (item.Status == _Defines.ACTIVE_STRING)
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
                             join subject in _context.Subjects on topic.SubjectId equals subject.SubjectId
                             where (topic.Grade == grade|| grade == null) && subject.SubjectId == subjectId && topic.TopicType == topicType 
                             where topic.IsDelete == false && topic.Status == _Defines.ACTIVE_STRING
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
                                     join question in _context.Questions on topic.TopicId equals question.TopicId
                                     join questionTest in _context.Questiontests on question.QuestionId equals questionTest.QuestionId
                                     join testDetail in _context.Testdetails on questionTest.TestDetailId equals testDetail.TestDetailId
                                     join account in _context.Accounts on testDetail.AccountId equals account.AccountId
                                     join subject in _context.Subjects on question.CourseChapterId equals subject.SubjectId
                                     where account.AccountId == accountId && testDetail.Submitted == true && topic.IsDelete == false
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
                topicDTO.SubjectName = item.SubjectName;
                topicDTO.SubjectId = item.SubjectId;
                var totalQuestion = (from topic in _context.Topics
                                     join question in _context.Questions
                                     on topic.TopicId equals question.TopicId
                                     where topic.TopicId == item.TopicId && question.Status == _Defines.ACTIVE_STRING
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
            var result = from topic in _context.Topics where topic.TopicId == topicId && topic.IsDelete == false
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
                        join question in _context.Questions on topic.TopicId equals question.TopicId
                        join questionTest in _context.Questiontests  on question.QuestionId equals questionTest.QuestionId
                        join testDetail in _context.Testdetails on questionTest.TestDetailId equals testDetail.TestDetailId
                        join account in _context.Accounts on testDetail.AccountId equals account.AccountId
                        join subject in _context.Subjects on topic.SubjectId equals subject.SubjectId
                        where topic.TopicId == topicId && topic.TopicType == topicType && topic.IsDelete == false
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
                        where topic.TopicType == topicType 
                        && topic.FinishTestDate >= DateTime.Now && topic.IsDelete == false && topic.Status ==_Defines.ACTIVE_STRING
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

        public async Task<string> DeleteTopicId(int TopicId, int AccountId)
        {
            try
            {
                if (TopicId > 0)
                {
                    if (AccountId > 0)
                    {
                        var data =await _context.Topics.Where(a => a.TopicId == TopicId && a.IsDelete == false).FirstOrDefaultAsync();
                        if(data != null)
                        {
                            data.IsDelete=true;
                            _context.Topics.Update(data);
                            await _context.SaveChangesAsync();
                            return "Delete topic success";
                        }
                        else
                        {
                            throw new Exception("Data is not exits.");
                        }
                    }
                    else
                    {
                        throw new Exception("Please select a Account Id.");
                    }
                }
                else
                {
                    throw new Exception("Please select a Id Topic.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<string> ComfirmInTopicId(int TopicId, int AccountId)
        {
            try
            {
                if (TopicId > 0)
                {
                    if (AccountId > 0)
                    {
                        var data = await _context.Topics.Where(a => a.TopicId == TopicId && a.IsDelete == false).FirstOrDefaultAsync();
                        if (data != null)
                        {
                            if(data.Status == _Defines.ACTIVE_STRING)
                            {
                                data.Status = _Defines.INACTIVE_STRING;
                                _context.Topics.Update(data);
                                await _context.SaveChangesAsync();
                                 return "Comfirm INACTIVE topic success";
                            }
                            else
                            {
                                data.Status = _Defines.ACTIVE_STRING;
                                _context.Topics.Update(data);
                                 await _context.SaveChangesAsync();
                                 return "Comfirm ACTIVE topic success";
                            }
                        }
                        else
                        {
                            throw new Exception("Data is not exits.");
                        }
                    }
                    else
                    {
                        throw new Exception("Please select a Account Id.");
                    }
                }
                else
                {
                    throw new Exception("Please select a Id Topic.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<string> DeleteQuestionInQuestionFromTopic(int QuestionId)
        {
            try
            {
                if (QuestionId > 0)
                {
                    var data = await _context.Questions.Where(a => a.QuestionId == QuestionId).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        if (data.CourseChapterId == null)
                        {
                            _context.Questions.Remove(data);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            data.TopicId = null;
                            data.DateUpdated = DateTime.Now;
                            _context.Questions.Update(data);
                            await _context.SaveChangesAsync();
                        }return "Delete success.";
                    }
                    else
                    {
                        throw new Exception("Data is not exits.");
                    }
                }
                else
                {
                    throw new Exception("Please select a QuestionId.");
                }
               

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
