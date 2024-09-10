using be.Models;
using be.Repositories.CouseCharter;
using Microsoft.EntityFrameworkCore;
//using be.Models;

namespace be.Repositories.ModRepository
{
    public class CouseCharterRepository : ICouseCharterRepository
    {
        private readonly SwtDbContext _context;

        public CouseCharterRepository()
        {
            _context = new SwtDbContext();
        }
        /// <summary>
        /// Load data Chương 
        /// </summary>
        /// <param name="ChapterTitleSearch"> tìm kiếm theo tên </param>
        /// <returns> list data Chương</returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<getCouseCharter>> GetAllListCouseCharter(string? ChapterTitleSearch)
        {
            try
            {
                var _Grades = _context.Grades.AsNoTracking();
                var _Subjects = _context.Subjects.AsNoTracking();
                var _Coursechapters = _context.Coursechapters.Where(a=>a.IsDelete == false).AsNoTracking();
                var result = await (from a in _Coursechapters
                                    where (string.IsNullOrEmpty(ChapterTitleSearch) == true) || a.ChapterTitle.ToLower().Contains(ChapterTitleSearch.ToLower())
                                    select new getCouseCharter()
                                    {
                                        AccountDelete = a.AccountDelete,
                                        AccountCreated = a.AccountCreated,
                                        DateCreated = a.DateCreated,
                                        DateDelete = a.DateDelete,
                                        DateUpdate = a.DateUpdate,
                                        AccountUpdate = a.AccountUpdate,
                                        Status = a.Status,
                                        ChapterId = a.ChapterId,
                                        Grade = _Grades.Where(z => z.GradeId == a.GradeId).FirstOrDefault(),
                                        Subject = _Subjects.Where(z => z.SubjectId == a.SubjecId).FirstOrDefault(),
                                        ChapterTitle = a.ChapterTitle,
                                        MainContent = a.MainContent,
                                        SubjecId = a.SubjecId,
                                    }).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        public async Task<List<getQuestionInCourseChapter>> GetAllListQuestionInCouseCharter(int IdCouseChapter, string? nameQuestion)
        {
            try
            {
                var _Grades = _context.Grades.AsNoTracking();
                var _CourseChapters = _context.Coursechapters.AsNoTracking();
                var _Answers = _context.Answers.AsNoTracking();
                var _Questions = _context.Questions.AsNoTracking();
                var _Level = _context.Levels.AsNoTracking();
                var _Topics = _context.Topics.AsNoTracking();
                var _Coursechapters = _context.Coursechapters.Where(a => a.IsDelete == false).AsNoTracking();
                var result = await (from a in _Questions
                                    where a.CourseChapterId ==IdCouseChapter 
                                    where string.IsNullOrEmpty(nameQuestion)==true || a.QuestionContext.ToLower().Contains(nameQuestion.ToLower())
                                    select new getQuestionInCourseChapter
                                    {
                                        QuestionId = a.QuestionId,
                                        QuestionContext = a.QuestionContext,
                                        Image = a.Image,
                                        OptionC = a.OptionC,
                                        OptionD = a.OptionD,
                                        Solution = a.Solution,
                                        Status = a.Status,
                                        UserCreated = a.UserCreated,
                                        DateCreated = a.DateCreated,
                                        UserDelete = a.UserDelete,
                                        DateDelete = a.DateDelete,
                                        UserUpdated = a.UserUpdated,
                                        DateUpdated = a.DateUpdated,
                                        OptionA = a.OptionA,
                                        OptionB = a.OptionB,
                                        Answer= _Answers.Where(z=>z.AnswerId == a.AnswerId).FirstOrDefault(),
                                        CourseChapter = _CourseChapters.Where(z=>z.ChapterId == a.CourseChapterId).FirstOrDefault(),
                                        CourseChapterId = a.CourseChapterId,
                                        Level = _Level.Where(z=>z.LevelId == a.LevelId).FirstOrDefault(),
                                        LevelId = a.LevelId,
                                        Topic = _Topics.Where(z=>z.TopicId == a.TopicId).FirstOrDefault(),
                                        TopicId = a.TopicId,
                                    }).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        public async Task<bool> ConfirmCourceCharter(int IdCouseChapter,int AccountUpdateId)
        {
            try
            {
                if(IdCouseChapter == 0)
                {
                    throw new Exception("IdCouseChapter is 0 or null");
                }
                var data =await _context.Coursechapters.Where(a=>a.ChapterId == IdCouseChapter&& a.IsDelete == false).FirstOrDefaultAsync();
                if(data == null)
                {
                    throw new Exception("No data found");
                }
                else
                {
                    if (data.Status == false)
                        data.Status = true;
                    else
                        data.Status = false;
                    data.DateUpdate = DateTime.UtcNow.AddHours(7);
                    data.AccountUpdate = AccountUpdateId;
                    _context.Coursechapters.Update(data);
                    await _context.SaveChangesAsync();
                }
                return true;
            }catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public async Task<bool> AddCourceCharter(Coursechapter model)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public async Task<bool> UpdateCourceCharter(Coursechapter model)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public async Task<bool> DeleteCourceCharter(int IdCouseChapter)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
    }
}
