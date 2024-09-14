using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using be.Helper;

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
                CourseChapterId = questionDTO.SubjectId,
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
                DateCreated = DateTime.Now
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
            var random = new Random();
            var shuffledList = result.OrderBy(x => random.Next()).ToList();
            var randomTenRows = shuffledList.Take(_defines.NUMBER_RANDOM_TOPIC).OrderByDescending(x => x.questionId).ToList();
            return new
            {
                message = "Get data successfully",
                status = 200,
                data = randomTenRows.OrderByDescending(x => x.questionId).ToList(),
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
    }
}
