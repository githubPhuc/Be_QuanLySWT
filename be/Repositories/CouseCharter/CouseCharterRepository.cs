using be.DTOs;
using be.Helper;
using be.Models;
using be.Repositories.CouseCharter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Linq;
//using be.Models;

namespace be.Repositories.ModRepository
{
    public class CouseCharterRepository : ICouseCharterRepository
    {
        private readonly SwtDbContext _context;
        private readonly Defines _defines;

        public CouseCharterRepository()
        {
            _context = new SwtDbContext();
            _defines = new Defines();
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
                                    where a.IsDelete == false
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
                                    }).OrderByDescending(a=>a.SubjecId).ThenByDescending(a=>a.DateCreated).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<List<getCouseCharter>> GetCouseCharterByGrade(int GradeId,int SubjectId,string? ChapterSearch)
        {
            try
            {
                var _Grades = _context.Grades.AsNoTracking();
                var _Subjects = _context.Subjects.AsNoTracking();
                var _Coursechapters = _context.Coursechapters.Where(a => a.IsDelete == false).AsNoTracking();
                var result = await (from a in _Coursechapters
                                    where a.GradeId == GradeId && a.SubjecId ==SubjectId
                                    where (string.IsNullOrEmpty(ChapterSearch) == true) || a.ChapterTitle.ToLower().Contains(ChapterSearch.ToLower())
                                    where a.IsDelete == false && a.Status == true
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
        public async Task<List<getQuestionInCourseChapter>> GetQuestionByCourseChaptersInUser(int IdCourseChapter)
        {
            try
            {
                var _Grades = _context.Grades.AsNoTracking();
                var _CourseChapters = _context.Coursechapters.AsNoTracking();
                var _Answers = _context.Answers.AsNoTracking();
                var _Questions = _context.Questions.AsNoTracking();
                var _Level = _context.Levels.AsNoTracking();
                var _Topics = _context.Topics.AsNoTracking();
                var _Coursechapters = _context.Coursechapters.Where(a => a.Status == true && a.IsDelete == false).AsNoTracking();
                if(_Coursechapters.Count()>0)
                {
                    var result = await (from a in _Questions
                                        where a.CourseChapterId == IdCourseChapter
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
                                            Answer = _Answers.Where(z => z.AnswerId == a.AnswerId).FirstOrDefault(),
                                            CourseChapter = _CourseChapters.Where(z => z.ChapterId == a.CourseChapterId).FirstOrDefault(),
                                            CourseChapterId = a.CourseChapterId,
                                            Level = _Level.Where(z => z.LevelId == a.LevelId).FirstOrDefault(),
                                            LevelId = a.LevelId,
                                            Topic = _Topics.Where(z => z.TopicId == a.TopicId).FirstOrDefault(),
                                            TopicId = a.TopicId,
                                        }).ToListAsync();

                    var random = new Random();
                    var shuffledList = result.OrderBy(x => random.Next()).ToList();
                    var randomTenRows = shuffledList.Take(_defines.NUMBER_RANDOM_COURSECHAPTER).ToList();
                    return randomTenRows;

                }
                else
                {
                    throw new Exception("Data does not exist or has been deleted");
                }
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
        public async Task<string> ConfirmCourceCharter(int IdCourceChapter, int AccountId)
        {
            try
            {
                if(IdCourceChapter == 0)
                {
                    throw new Exception("IdCouseChapter is 0 or null");
                }
                var data =await _context.Coursechapters.Where(a=>a.ChapterId == IdCourceChapter && a.IsDelete == false).FirstOrDefaultAsync();
                if(data == null)
                {
                    throw new Exception("No data found");
                }
                else
                {
                    string Mess = "";
                    if (data.Status == false)
                    {
                        data.Status = true;
                        Mess = $"Active {data.ChapterTitle} success.";
                    }
                    else
                    { 
                        data.Status = false;
                        Mess = $"Inactive {data.ChapterTitle} success.";
                    }
                    data.DateUpdate = DateTime.UtcNow.AddHours(7);
                    data.AccountUpdate = AccountId;
                    _context.Coursechapters.Update(data);
                    await _context.SaveChangesAsync();
                    return Mess;
                }
            }catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public async Task<string> AddCourceCharter(PostDataInsertCourseChapter model)
        {
            try
            {
                if(!string.IsNullOrEmpty(model.ChapterTitle))
                {
                    if(model.SubjecId == 0)
                    {
                        throw new Exception("Subjects cannot be left blank.");
                    }
                    else
                    {
                        if (model.GradeId == 0)
                        {
                            throw new Exception("GradeId cannot be left blank.");
                        }
                        else
                        {
                            var data = new Coursechapter()
                            {
                                DateCreated = DateTime.UtcNow.AddHours(7),
                                DateUpdate = DateTime.UtcNow.AddHours(7),
                                DateDelete = DateTime.UtcNow.AddHours(7),
                                AccountUpdate = model.AccountId,
                                AccountCreated = model.AccountId,
                                AccountDelete = model.AccountId,
                                IsDelete = false,
                                Status = false,
                                ChapterTitle = model.ChapterTitle,
                                MainContent = model.MainContent,
                                GradeId = model.GradeId,
                                SubjecId = model.SubjecId,

                            };
                            _context.Coursechapters.Add(data);
                            await _context.SaveChangesAsync();
                            return "Add cource chapter success.";
                        }
                    }
                }
                else
                {
                    throw new Exception("Chapter title cannot be left blank.");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public async Task<string> UpdateCourceCharter(int IdCourceChapter,PostDataInsertCourseChapter model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.ChapterTitle))
                {
                    if (model.SubjecId == 0)
                    {
                        throw new Exception("Subjects cannot be left blank.");
                    }
                    else
                    {
                        if (model.GradeId == 0)
                        {
                            throw new Exception("GradeId cannot be left blank.");
                        }
                        else
                        {
                            var data =await _context.Coursechapters.Where(a => a.ChapterId == IdCourceChapter).FirstOrDefaultAsync();
                            if(data == null)
                            {
                                throw new Exception("Data already exists.");
                            }
                            data.DateUpdate = DateTime.UtcNow.AddHours(7);
                            data.AccountUpdate = model.AccountId;
                            data.ChapterTitle = model.ChapterTitle;
                            data.MainContent = model.MainContent;
                            data.GradeId = model.GradeId;
                            data.SubjecId = model.SubjecId;
                            _context.Coursechapters.Update(data);
                            await _context.SaveChangesAsync();
                            return "Update cource chapter success.";
                        }
                    }
                }
                else
                {
                    throw new Exception("Chapter title cannot be left blank.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public async Task<string> DeleteCourceCharter(int IdCourceChapter,int AccountId)
        {
            try
            {
                var data = await _context.Coursechapters.Where(a => a.ChapterId == IdCourceChapter).FirstOrDefaultAsync();
                if (data == null)
                {
                    throw new Exception("Data already exists.");
                }
                data.DateDelete = DateTime.UtcNow.AddHours(7);
                data.AccountDelete = AccountId;
                data.IsDelete = true;
                _context.Coursechapters.Update(data);
                await _context.SaveChangesAsync();
                return $"Delete {data.ChapterTitle} success.";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }




        public async Task<string> AddQuestionInCourseChapterID(PostDataInsertQuestionInCourseChapterID model)
        {
            try
            {
                var _Coursechapters = await _context.Coursechapters.Where(a => a.ChapterId == model.CourseChapterId).FirstOrDefaultAsync();
                if (_Coursechapters == null)
                {
                    throw new Exception("Data course chapters already exists.");
                }
                else
                {
                    var data = new Question()
                    {
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        DateUpdated = DateTime.UtcNow.AddHours(7),
                        DateDelete = DateTime.UtcNow.AddHours(7),
                        CourseChapterId = _Coursechapters.ChapterId,
                        UserCreated = model.AccountId,
                        UserUpdated = model.AccountId,
                        UserDelete = model.AccountId,
                        QuestionContext = model.QuestionContext,
                        AnswerId = model.AnswerId,
                        OptionA = model.OptionA,
                        OptionB= model.OptionB,
                        OptionC = model.OptionC,
                        OptionD = model.OptionD,
                        Solution = model.Solution,
                        Image = model.Image,
                        Status = "Inactive",
                        IsDelete = false,
                        LevelId = model.LevelId,
                    };
                    _context.Questions.Add(data);
                    await _context.SaveChangesAsync();
                    return $"Add {data.QuestionContext} success.";

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public async Task<string> UpdateQuestionInCourseChapterID(PostDataInsertQuestionInCourseChapterID model,int QuestionId)
        {
            try
            {
                var _Coursechapters = await _context.Coursechapters.Where(a => a.ChapterId == model.CourseChapterId).FirstOrDefaultAsync();
                if (_Coursechapters == null)
                {
                    throw new Exception("Data course chapters already exists.");
                }
                else
                {
                    var data = await _context.Questions.Where(a => a.QuestionId == QuestionId).FirstOrDefaultAsync();
                    if (data == null)
                    {
                        throw new Exception("Data question already exists.");
                    }
                    else
                    {

                        data.DateUpdated = DateTime.UtcNow.AddHours(7);
                        data.UserUpdated = model.AccountId;
                        data.QuestionContext = model.QuestionContext;
                        data.AnswerId = model.AnswerId;
                        data.OptionA = model.OptionA;
                        data.OptionB = model.OptionB;
                        data.OptionC = model.OptionC;
                        data.OptionD = model.OptionD;
                        data.Solution = model.Solution;
                        data.Image = model.Image;
                        data.Status = "Active";
                        data.LevelId = model.LevelId;
                        _context.Questions.Update(data);
                        await _context.SaveChangesAsync();
                        return $"Update {data.QuestionContext} success.";
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public async Task<string> DeleteQuestionInCourseChapterID(int QuestionId,int AccountId)
        {
            try
            {
               
                    var data = await _context.Questions.Where(a => a.QuestionId == QuestionId).FirstOrDefaultAsync();
                    if (data == null)
                    {
                        throw new Exception("Data question already exists.");
                    }
                    else
                    {

                        
                        _context.Questions.Remove(data);
                        await _context.SaveChangesAsync();
                        return $"Delete {data.QuestionContext} success.";
                    }

                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public async Task<string> AddExcelQuestionInCourseChapterID(int CourseChapterID, int AccountId)
        {
            try
            {

                var data = await _context.Coursechapters.Where(a => a.ChapterId == CourseChapterID).FirstOrDefaultAsync();
                if (data == null)
                {
                    throw new Exception("Data Coursechapters already exists.");
                }
                else
                {
                    _context.Coursechapters.Add(data);
                    await _context.SaveChangesAsync();
                    return $"Add success.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
    }
}
