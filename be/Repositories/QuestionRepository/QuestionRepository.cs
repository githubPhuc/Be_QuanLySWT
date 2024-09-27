using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using be.Helper;
using be.Repositories.CouseCharter;
using OfficeOpenXml;

namespace be.Repositories.QuestionRepository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly SwtDbContext _context;
        private readonly Defines _defines;
        public QuestionRepository()
        {
            _context = new SwtDbContext();
            _defines = new Defines();
        }

        public void AddQuestionByExcel(Question question)
        {
            question.Status = _defines.ACTIVE_STRING;
            question.AccountId = question.AccountId;
            question.UserCreated = question.AccountId;
            question.UserDelete = question.AccountId;
            question.UserUpdated = question.AccountId;
            question.DateCreated = DateTime.Now;
            question.DateDelete = DateTime.Now;
            question.DateUpdated = DateTime.Now;
            question.IsDelete = false;
            _context.Questions.Add(question);
            _context.SaveChanges();

            var count = _context.Questions.Where(x => x.TopicId == question.TopicId).Count();
            var topic = _context.Topics.SingleOrDefault(x => x.TopicId == question.TopicId);

            if (topic != null)
            {
                topic.TotalQuestion = count;
                _context.SaveChanges();
            }
        }
        public async Task<string> AddExcelQuestionInTopicID(IFormFile file, int AccountId, int TopicID)
        {
            try
            {
                var checkCource = await _context.Topics.Where(a => a.TopicId == TopicID).FirstOrDefaultAsync();
                if (checkCource == null)
                {
                    throw new Exception("Topic not exits.");
                }
                if (file == null || file.Length == 0) { throw new Exception("File not exist!!"); }
                else
                {
                    string ExtensionFile = Path.GetExtension(Path.GetFileName(file.FileName));
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    fileName = Guid.NewGuid().ToString() + ExtensionFile;

                    if ((ExtensionFile == ".xlsx") || (ExtensionFile == ".xls"))
                    {
                        using (var stream = new MemoryStream())
                        {
                            file.CopyTo(stream);
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            using (var package = new ExcelPackage(stream))
                            {
                                var worksheet = package.Workbook.Worksheets[0];
                                if (worksheet.Dimension == null)
                                {
                                    throw new Exception("The worksheet is empty!!");
                                }
                                else
                                {
                                    int totalRows = worksheet.Dimension.Rows;
                                    var dataToInsert = new List<ExcelUploadQuestion>();
                                    var uniqueDataDictionary = new Dictionary<string, int>();
                                    for (int row = 5; row <= totalRows; row++)
                                    {
                                        var data_ = new ExcelUploadQuestion
                                        {
                                            UrlImage = worksheet.Cells[row, 2].Value?.ToString() ?? "",
                                            QuestionContext = worksheet.Cells[row, 3].Value?.ToString() ?? "",
                                            OptionA = worksheet.Cells[row, 4].Value?.ToString() ?? "",
                                            OptionB = worksheet.Cells[row, 5].Value?.ToString() ?? "",
                                            OptionC = worksheet.Cells[row, 6].Value?.ToString() ?? "",
                                            OptionD = worksheet.Cells[row, 7].Value?.ToString() ?? "",
                                            Solution = worksheet.Cells[row, 8].Value?.ToString() ?? "",
                                            Answer = worksheet.Cells[row, 9].Value?.ToString() ?? "",
                                            LevelName = worksheet.Cells[row, 1].Value?.ToString() ?? ""
                                        };
                                        string uniqueKey = $"{data_.UrlImage}-{data_.QuestionContext}-{data_.OptionA}-{data_.OptionB}-{data_.OptionC}-{data_.OptionD}-{data_.Solution}-{data_.LevelName}";

                                        if (!uniqueDataDictionary.ContainsKey(uniqueKey))
                                        {
                                            uniqueDataDictionary.Add(uniqueKey, row);
                                            dataToInsert.Add(data_);
                                        }
                                    }
                                    var _Answer = _context.Answers.AsNoTracking();
                                    var _Levels = _context.Levels.AsNoTracking();
                                    var _Rerult = new List<Question>();
                                    foreach (var item in dataToInsert)
                                    {
                                        if (!string.IsNullOrEmpty(item.QuestionContext) &&
                                            !string.IsNullOrEmpty(item.OptionA) &&
                                            !string.IsNullOrEmpty(item.OptionB) &&
                                            !string.IsNullOrEmpty(item.OptionC) &&
                                            !string.IsNullOrEmpty(item.OptionD))
                                        {
                                            _Rerult.Add(new Question()
                                            {
                                                DateCreated = DateTime.UtcNow.AddHours(7),
                                                DateUpdated = DateTime.UtcNow.AddHours(7),
                                                DateDelete = DateTime.UtcNow.AddHours(7),
                                                TopicId = TopicID,
                                                AccountId = AccountId,
                                                UserCreated = AccountId,
                                                UserUpdated = AccountId,
                                                UserDelete = AccountId,
                                                QuestionContext = item.QuestionContext,
                                                AnswerId = _Answer.Where(a => a.AnswerName == item.Answer).FirstOrDefault()?.AnswerId ?? 0,
                                                OptionA = item.OptionA,
                                                OptionB = item.OptionB,
                                                OptionC = item.OptionC,
                                                OptionD = item.OptionD,
                                                Solution = item.Solution,
                                                Image = item.UrlImage,
                                                Status = _defines.ACTIVE_STRING,
                                                IsDelete = false,
                                                LevelId = _Levels.Where(a => a.LevelName == item.LevelName).FirstOrDefault()?.LevelId ?? 0,
                                            });
                                        }
                                    }
                                    if (_Rerult.Count() > 0)
                                    {
                                        _context.ChangeTracker.AutoDetectChangesEnabled = false;
                                        await _context.Questions.AddRangeAsync(_Rerult);
                                        await _context.SaveChangesAsync();
                                        _context.ChangeTracker.AutoDetectChangesEnabled = true;
                                    }
                                    return $"Upload success {_Rerult.Count()} question";
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Not an excel file, please try again!!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object ApproveAllQuestionOfTopic(int topicId)
        {
            var questionList = _context.Questions.Where(x => x.TopicId == topicId && x.Status == _defines.INACTIVE_STRING).ToList();

            foreach (var question in questionList)
            {
                question.Status = _defines.ACTIVE_STRING;
            }

            try
            {
                _context.SaveChanges();
                return new
                {
                    message = "Change status successfully",
                    status = 200,
                };
            }
            catch
            {
                return new
                {
                    message = "Change status failed",
                    status = 400,
                };
            }
        }

        public object ChangeStatusQuestion(int questionId, string status)
        {
            var question = _context.Questions.SingleOrDefault(x => x.QuestionId == questionId);
            if (question == null)
            {
                return new
                {
                    message = "No data found",
                    status = 400,
                };
            }
            question.Status = status;
            _context.SaveChanges();
            return new
            {
                message = "Change status successfully",
                status = 200,
                data = question,
            };
        }

        public object CreateQuestion(CreateQuestionDTO questionDTO)
        {
            var question = new Question
            {
                //CourseChapterId = questionDTO.SubjectId,
                AccountId = questionDTO.AccountId,
                AnswerId = questionDTO.AnswerId,
                LevelId = questionDTO.LevelId,
                TopicId = questionDTO.TopicId,
                Image = questionDTO.Image,
                QuestionContext = questionDTO.QuestionContent,
                OptionA = questionDTO.OptionA,
                OptionB = questionDTO.OptionB,
                OptionC = questionDTO.OptionC,
                OptionD = questionDTO.OptionD,
                Solution = questionDTO.Solution,
                Status = _defines.ACTIVE_STRING,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                DateDelete = DateTime.Now,
                IsDelete = false,
                UserCreated = questionDTO.AccountId,
                UserDelete = questionDTO.AccountId,
                UserUpdated = questionDTO.AccountId,
            };

            try
            {
                _context.Questions.Add(question);
                _context.SaveChanges();

                var topic = _context.Topics.SingleOrDefault(x => x.TopicId == questionDTO.TopicId);
                if (topic != null)
                {
                    topic.TotalQuestion = _context.Questions.Count(x => x.TopicId == questionDTO.TopicId);
                    _context.SaveChanges();
                }

                return new
                {
                    message = "Add Successfully",
                    status = 200,
                };
            }
            catch
            {
                return new
                {
                    message = "Add Failed",
                    status = 400,
                };
            }
        }

        public object EditQuestion(EditQuestionDTO questionDTO)
        {
            var question = _context.Questions.SingleOrDefault(x => x.QuestionId == questionDTO.QuestionId);
            if (question == null)
            {
                return new
                {
                    message = "Not found",
                    status = 400,
                };
            }
            try
            {
                question.LevelId = questionDTO.LevelId;
                question.QuestionContext = questionDTO.QuestionContent;
                question.Image = questionDTO.Image;
                question.OptionA = questionDTO.OptionA;
                question.OptionB = questionDTO.OptionB;
                question.OptionC = questionDTO.OptionC;
                question.OptionD = questionDTO.OptionD;
                question.AnswerId = questionDTO.AnswerId;
                question.Solution = questionDTO.Solution;
                question.DateUpdated = DateTime.Now;
                _context.Questions.Update(question);
                _context.SaveChanges();

                return new
                {
                    message = "Edit Question Successfully",
                    status = 200,
                    question,
                };
            }
            catch
            {
                return new
                {
                    message = "Edit Question Failed",
                    status = 400,
                };
            }
        }

        public object GetAllQuestionByTopicId(int topicId)
        {
            var query = from question in _context.Questions
                        where question.TopicId == topicId
                        select question;
            var result = new List<QuestionByTopicIdDTO>();
            foreach(var question in query.ToList())
            {
                var topic = _context.Topics.SingleOrDefault(x => x.TopicId == question.TopicId);    
                QuestionByTopicIdDTO questionByTopicIdDTO = new QuestionByTopicIdDTO();
                var accountId = _context.Accounts.SingleOrDefault(x => x.AccountId == question.AccountId);  
                var answer = _context.Answers.SingleOrDefault(x => x.AnswerId == question.AnswerId);
                var level = _context.Levels.SingleOrDefault(x => x.LevelId == question.LevelId);
                var subject = _context.Subjects.SingleOrDefault(x => x.SubjectId == topic.SubjectId);    
                
                questionByTopicIdDTO.questionId = question.QuestionId;
                questionByTopicIdDTO.subjectId = subject.SubjectId;
                questionByTopicIdDTO.subjectName = subject.SubjectName;
                questionByTopicIdDTO.questionContent = question.QuestionContext;
                questionByTopicIdDTO.accountId = accountId.AccountId;
                questionByTopicIdDTO.accountName = accountId.FullName;
                questionByTopicIdDTO.answerId = answer.AnswerId;
                questionByTopicIdDTO.answer = answer.AnswerName;
                questionByTopicIdDTO.level = level.LevelName;
                questionByTopicIdDTO.levelId = level.LevelId;
                questionByTopicIdDTO.topic = topic.TopicName;
                questionByTopicIdDTO.topicId = topic.TopicId;
                questionByTopicIdDTO.image = question.Image;
                questionByTopicIdDTO.optionA = question.OptionA;
                questionByTopicIdDTO.optionB = question.OptionB;
                questionByTopicIdDTO.optionC = question.OptionC;
                questionByTopicIdDTO.optionD = question.OptionD;
                questionByTopicIdDTO.solution = question.Solution;
                questionByTopicIdDTO.status = question.Status;
                questionByTopicIdDTO.createDate = question.DateCreated;
                questionByTopicIdDTO.statusString = question.Status == _defines.INACTIVE_STRING ? "Chờ duyệt" : question.Status == _defines.ACTIVE_STRING ? "Đã duyệt" : "Khóa";
                result.Add(questionByTopicIdDTO);
            }

            if (result == null)
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
                data = result.ToList(),
            };
        }
        




        public async Task<object> GetQuestionByTopicId(int topicId)
        {
            var data = await (from question in _context.Questions
                              join answer in _context.Answers
                              on question.AnswerId equals answer.AnswerId
                              join level in _context.Levels
                              on question.LevelId equals level.LevelId
                              join topic in _context.Topics
                              on question.TopicId equals topic.TopicId
                              where question.TopicId == topicId && question.Status == _defines.ACTIVE_STRING
                              where topic.IsDelete == false
                              select new
                              {
                                  topicId = topic.TopicId,
                                  topicName = topic.TopicName,
                                  questionId = question.QuestionId,
                                  subjectId = question.CourseChapterId,
                                  answerId = question.AnswerId,
                                  answerName = answer.AnswerName,
                                  levelId = question.LevelId,
                                  levelName = level.LevelName,
                                  image = question.Image,
                                  questionContext = question.QuestionContext,
                                  optionA = question.OptionA,
                                  optionB = question.OptionB,
                                  optionC = question.OptionC,
                                  optionD = question.OptionD,
                                  solution = question.Solution,
                                  isRight = false,
                              }).OrderBy(x => x.questionId).ToListAsync();

            return new
            {
                status = 200,
                data,
            };
        }
        public async Task<object> GetQuestionByTopicIdInUser(int topicId)
        {
            var topics =await _context.Topics.Where(a => a.TopicId == topicId).FirstOrDefaultAsync();
            var data = await (from question in _context.Questions
                              join answer in _context.Answers
                              on question.AnswerId equals answer.AnswerId
                              join level in _context.Levels
                              on question.LevelId equals level.LevelId
                              join topic in _context.Topics
                              on question.TopicId equals topic.TopicId
                              where question.TopicId == topicId && question.Status == _defines.ACTIVE_STRING
                              where topic.IsDelete == false
                              select new
                              {
                                  topicId = topic.TopicId,
                                  topicName = topic.TopicName,
                                  questionId = question.QuestionId,
                                  subjectId = question.CourseChapterId,
                                  answerId = question.AnswerId,
                                  answerName = answer.AnswerName,
                                  levelId = question.LevelId,
                                  levelName = level.LevelName,
                                  image = question.Image,
                                  questionContext = question.QuestionContext,
                                  optionA = question.OptionA,
                                  optionB = question.OptionB,
                                  optionC = question.OptionC,
                                  optionD = question.OptionD,
                                  solution = question.Solution,
                                  isRight = false,
                              }).OrderBy(x => x.questionId).ToListAsync();
            int Numrandom = 0;
            switch (topics?.TopicType??0)
            {
                case 1:
                    Numrandom = _defines.NUMBER_RANDOM_TOPIC_1;
                    break;
                case 2:
                    Numrandom = _defines.NUMBER_RANDOM_TOPIC_2;
                    break;
                case 3:
                    Numrandom = _defines.NUMBER_RANDOM_TOPIC_3;
                    break;
                case 4:
                    Numrandom = _defines.NUMBER_RANDOM_TOPIC_4;
                    break;
                case 5:
                    Numrandom = _defines.NUMBER_RANDOM_TOPIC_5;
                    break;
                case 6:
                    Numrandom = _defines.NUMBER_RANDOM_TOPIC_6;
                    break;
                default:
                    Numrandom = 10;
                    break;
            }
            var random = new Random();
            var shuffledList = data.OrderBy(x => random.Next()).ToList();
            var randomTenRows = shuffledList.Take(Numrandom).OrderByDescending(x => x.questionId).ToList();
            return new
            {
                message = "Get data successfully",
                status = 200,
                data = randomTenRows.OrderByDescending(x => x.questionId).ToList(),
            };
        }
    }
}
