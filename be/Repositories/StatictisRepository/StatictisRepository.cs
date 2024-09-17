using be.DTOs;
using be.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Globalization;
using System.Linq;

namespace be.Repositories.StatictisRepository
{
    public class StatictisRepository : IStatictisRepository
    {
        private readonly SwtDbContext _context;

        public StatictisRepository()
        {
            _context = new SwtDbContext();
        }

        public object GetAllQuestionDoneByUser(int userId)
        {
            try
            {
                var totalQuestionDone = 0;
                double totalScore = 0;
                var totalTime = 0;
                var testDetailByUser = _context.Testdetails.Where(x => x.AccountId == userId).ToList();
                if(testDetailByUser.Count() == 0)
                {
                    return new
                    {
                        message = "Successfully",
                        status = 200,
                        totalQuestion = 0,
                        totalScoreAverage = 0,
                        totalTime = 0,
                    };
                }
                foreach (var test in testDetailByUser)
                {
                    try
                    {
                        var totalQuestionInTest = _context.Questiontests.Where(x => x.TestDetailId == test.TestDetailId).Count();
                        totalQuestionDone += totalQuestionInTest;
                        totalScore += (double)test.Score;
                        var firstQuestionInTest = _context.Questiontests.Where(x => x.TestDetailId == test.TestDetailId).FirstOrDefault();
                        var question = _context.Questions.FirstOrDefault(x => x.QuestionId == firstQuestionInTest.QuestionId);
                        var topic = _context.Topics.FirstOrDefault(x => x.TopicId == question.TopicId);
                        if (topic.Duration.Equals("15"))
                        {
                            totalTime += 15;
                        }
                        else if (topic.Duration.Equals("45"))
                        {
                            totalTime += 45;
                        }
                        else if (topic.Duration.Equals("60"))
                        {
                            totalTime += 60;
                        }
                        else
                        {
                            totalTime += 120;
                        }
                    }
                    catch { continue; }
                }
                return new
                {
                    message = "Successfully",
                    status = 200,
                    totalQuestion = totalQuestionDone,
                    totalScoreAverage = Math.Round(totalScore / testDetailByUser.Count(), 2),
                    totalTime = totalTime,
                };
            }
            catch(Exception ex)
            {
                return new
                {
                    message = "Failed "+ex.Message,
                    status = 400,
                };
            }
            
            
        }

        public object GetTestDetails()
        {
            var testHistory = new List<HistoryDTO>();
            var testDetailByAccountId = _context.Testdetails.ToList().OrderByDescending(x => x.TestDetailId);
            foreach (var testDetail in testDetailByAccountId)
            {
                var historyDTO = new HistoryDTO();
                historyDTO.TestDetailId = testDetail.TestDetailId;
                historyDTO.SubmitDate = (DateTime)testDetail.DateCreated;
                var getQuestion = _context.Questiontests.Where(x => x.TestDetailId == testDetail.TestDetailId).FirstOrDefault();
                var question = _context.Questions.SingleOrDefault(x => x.QuestionId == getQuestion.QuestionId);
                var subject = _context.Subjects.SingleOrDefault(x => x.SubjectId == question.CourseChapterId);
                historyDTO.SubjectName = subject.SubjectName;
                var topic = _context.Topics.SingleOrDefault(x => x.TopicId == question.TopicId);
                historyDTO.Topic = topic.TopicName;
                testHistory.Add(historyDTO);
            }

            var result = testHistory
       .GroupBy(h => new { h.SubmitDate.Date, h.SubjectName })
       .Select(group => new
       {
           SubmitDate = group.Key.Date,
           SubjectName = group.Key.SubjectName,
           Count = group.Count()
       })
       .ToList();
            if (result == null)
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
                data = result
            };
        }



        #region - Statics User
        public object StaticsticUser()
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                var totalUser = TotalUser();
                var totalByMonth = TotalUserRegisterInMonth(dateTime.Year, dateTime.Month);
                var totalByYear = TotalUserRegisterInYear(dateTime.Year);
                var totalByDay = TotalUserRegisterInDay(dateTime.Day, dateTime.Month, dateTime.Year);
                return new
                {
                    message = "Successfully",
                    status = 200,
                    totalUser = totalUser,
                    totalByMonth = totalByMonth,
                    totalByYear = totalByYear,
                    totalByDay = totalByDay,
                };
            }
            catch
            {
                return new
                {
                    message = "Failly",
                    status = 400,
                };
            }

        }
        public int TotalUser()
        {
            var countUser = _context.Accounts.Where(x => x.RoleId == 4).Count();
            return countUser;
        }

        public int TotalUserRegisterInMonth(int year, int month)
        {
            int countUser = _context.Accounts
                .Where(x => x.RoleId == 4 &&
                            x.DateCreated.HasValue &&
                            x.DateCreated.Value.Year == year &&
                            x.DateCreated.Value.Month == month)
                .Count();

            return countUser;
        }

        public int TotalUserRegisterInYear(int year)
        {
            int countUser = _context.Accounts
                .Where(x => x.RoleId == 4 &&
                            x.DateCreated.HasValue &&  // Kiểm tra DateCreated có giá trị không rỗng
                            x.DateCreated.Value.Year == year)
                .Count();

            return countUser;
        }

        public int TotalUserRegisterInDay(int day, int month, int year)
        {
            int countUser = _context.Accounts
                .Where(x => x.RoleId == 4 &&
                            x.DateCreated.HasValue &&  // Kiểm tra DateCreated có giá trị không rỗng
                            x.DateCreated.Value.Year == year &&
                            x.DateCreated.Value.Month == month &&
                            x.DateCreated.Value.Day == day)
                .Count();

            return countUser;
        }

        public object StatictisUserByMonth(int? year)
        {
            try
            {
                int currentYear = year ?? DateTime.Now.Year;

                var data = Enumerable.Range(0, 13)
                    .Select(month => new MonthData
                    {
                        Month = month,
                        Value = _context.Accounts
                            .Count(x => x.RoleId == 4 &&
                                        x.DateCreated.HasValue &&
                                        x.DateCreated.Value.Year == currentYear &&
                                        x.DateCreated.Value.Month == month)
                    })
                    .ToList();
                return new
                {
                    message = "Successfully",
                    status = 200,
                    data,
                };
            }
            catch
            {
                return new
                {
                    message = "Failly",
                    status = 400,
                };
            }
        }

        public object StatisticsUserByDay(int? month)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                int currentYear = currentDate.Year;
                int selectedMonth = month ?? currentDate.Month; 
                int daysInSelectedMonth = DateTime.DaysInMonth(currentYear, selectedMonth);

                var data = Enumerable.Range(0, daysInSelectedMonth + 1)
                    .Select(day => new DayData
                    {
                        Day = day,
                        Value = _context.Accounts
                            .Count(x => x.RoleId == 4 &&
                                        x.DateCreated.HasValue &&
                                        x.DateCreated.Value.Year == currentYear &&
                                        x.DateCreated.Value.Month == selectedMonth &&
                                        x.DateCreated.Value.Day == day)
                    })
                    .ToList();

                return new
                {
                    message = "Successfully",
                    status = 200,
                    data,
                };
            }
            catch
            {
                return new
                {
                    message = "Failly",
                    status = 400,
                };
            }
        }

        #endregion

        #region - Statistics Topic
        public object StatictsticTopic()
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                var totalTopic = TotalTopic();
                var totalByMonth = TotalTopicInMonth(dateTime.Year, dateTime.Month);
                var totalByYear = TotalTopicInYear(dateTime.Year);
                var totalByDay = TotalTopicInDay(dateTime.Day, dateTime.Month, dateTime.Year);
                return new
                {
                    message = "Successfully",
                    status = 200,
                    totalTopic = totalTopic,
                    totalByMonth = totalByMonth,
                    totalByYear = totalByYear,
                    totalByDay = totalByDay,
                };
            }
            catch
            {
                return new
                {
                    message = "Failly",
                    status = 400,
                };
            }
        }

        public int TotalTopic()
        {
            return _context.Topics.Where(x => x.TopicType != 1).Count();
        }

        public int TotalTopicInMonth(int year, int month)
        {
            int countTopic = _context.Topics
                .Where(x => x.TopicType != 1 &&
                            x.DateCreated.HasValue &&
                            x.DateCreated.Value.Year == year &&
                            x.DateCreated.Value.Month == month)
                .Count();

            return countTopic;
        }

        public int TotalTopicInYear(int year)
        {
            int countTopic = _context.Topics
                .Where(x => x.TopicType != 1 &&
                            x.DateCreated.HasValue &&
                            x.DateCreated.Value.Year == year)
                .Count();

            return countTopic;
        }

        public int TotalTopicInDay(int day, int month, int year)
        {
            int countTopic = _context.Topics
                .Where(x => x.TopicType != 1 &&
                            x.DateCreated.HasValue && 
                            x.DateCreated.Value.Year == year &&
                            x.DateCreated.Value.Month == month &&
                            x.DateCreated.Value.Day == day)
                .Count();

            return countTopic;
        }

        public object StatictisTopicByMonth(int? year)
        {
            try
            {
                int currentYear = year ?? DateTime.Now.Year;

                var data = Enumerable.Range(0, 13)
                    .Select(month => new MonthData
                    {
                        Month = month,
                        Value = _context.Topics
                            .Count(x => x.TopicType != 1 &&
                                        x.DateCreated.HasValue &&
                                        x.DateCreated.Value.Year == currentYear &&
                                        x.DateCreated.Value.Month == month)
                    })
                    .ToList();
                return new
                {
                    message = "Successfully",
                    status = 200,
                    data,
                };
            }
            catch
            {
                return new
                {
                    message = "Failly",
                    status = 400,
                };
            }
        }


        #endregion

        #region - function - statictis Question
        public object StatictsticQuestion()
        {
            try
            {
                var listSubject = _context.Subjects.ToList();
                List<DataQuestion> data = new List<DataQuestion>();
                foreach(var item in listSubject)
                {
                    DataQuestion dataQuestion = new DataQuestion();
                    dataQuestion.SubjectName = item.SubjectName;
                    dataQuestion.TotalQuestion = _context.Questions.Where(x => x.CourseChapterId == item.SubjectId).Count();
                    data.Add(dataQuestion);
                }
                return new
                {
                    message = "Successfully",
                    status = 200,
                    data,
                };
            }
            catch
            {
                return new
                {
                    message = "Failly",
                    status = 400,
                };
            }
        }

        public object StatictisQuestionBySubject(int? subjectId)
        {
            try
            {
                List<DataQuestionByType> dataQuestionByTypes = new List<DataQuestionByType>();
                if (subjectId == null)
                {
                    DataQuestionByType dataQuestionType1 = new DataQuestionByType();
                    dataQuestionType1.QuestionType = "Thông hiểu";
                    dataQuestionType1.TotalQuestion = _context.Questions.Where(x => x.LevelId == 1).Count();
                    dataQuestionByTypes.Add(dataQuestionType1);
                    DataQuestionByType dataQuestionType2 = new DataQuestionByType();
                    dataQuestionType2.QuestionType = "Vận dụng thấp";
                    dataQuestionType2.TotalQuestion = _context.Questions.Where(x => x.LevelId == 2).Count();
                    dataQuestionByTypes.Add(dataQuestionType2);
                    DataQuestionByType dataQuestionType3 = new DataQuestionByType();
                    dataQuestionType3.QuestionType = "Vận dụng cao";
                    dataQuestionType3.TotalQuestion = _context.Questions.Where(x => x.LevelId == 3).Count();
                    dataQuestionByTypes.Add(dataQuestionType3);
                }
                else
                {
                    DataQuestionByType dataQuestionType1 = new DataQuestionByType();
                    dataQuestionType1.QuestionType = "Thông hiểu";
                    dataQuestionType1.TotalQuestion = _context.Questions.Where(x => x.LevelId == 1 && x.CourseChapterId == subjectId).Count();
                    dataQuestionByTypes.Add(dataQuestionType1);
                    DataQuestionByType dataQuestionType2 = new DataQuestionByType();
                    dataQuestionType2.QuestionType = "Vận dụng thấp";
                    dataQuestionType2.TotalQuestion = _context.Questions.Where(x => x.LevelId == 2 && x.CourseChapterId == subjectId).Count();
                    dataQuestionByTypes.Add(dataQuestionType2);
                    DataQuestionByType dataQuestionType3 = new DataQuestionByType();
                    dataQuestionType3.QuestionType = "Vận dụng cao";
                    dataQuestionType3.TotalQuestion = _context.Questions.Where(x => x.LevelId == 3 && x.CourseChapterId == subjectId).Count();
                    dataQuestionByTypes.Add(dataQuestionType3);
                }
                return new
                {
                    message = "Successfully",
                    status = 200,
                    data = dataQuestionByTypes,
                };
            }
            catch
            {
                return new
                {
                    message = "Failly",
                    status = 400,
                };
            }
            
        }

       
        #endregion
    }
}
