using be.DTOs;
using be.Helper;
using be.Models;
using be.Repositories.CouseCharter;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel;
using LicenseContext = OfficeOpenXml.LicenseContext;
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
                                        AnswerId = a.AnswerId,
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
        public async Task<string> AddExcelQuestionInCourseChapterID(IFormFile file, int AccountId,int CourseChapterID)
        {
            try
            {
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
                                    for (int row = 2; row <= totalRows; row++)
                                    {
                                        var data_ = new ExcelUploadQuestion
                                        {
                                            UrlImage = worksheet.Cells[row, 1].Value?.ToString() ?? "",
                                            QuestionContext = worksheet.Cells[row, 2].Value?.ToString() ?? "",
                                            OptionA = worksheet.Cells[row, 3].Value?.ToString() ?? "",
                                            OptionB = worksheet.Cells[row, 4].Value?.ToString() ?? "",
                                            OptionC = worksheet.Cells[row, 5].Value?.ToString() ?? "",
                                            OptionD = worksheet.Cells[row, 6].Value?.ToString() ?? "",
                                            Solution = worksheet.Cells[row, 7].Value?.ToString() ?? "",
                                            Answer = worksheet.Cells[row, 8].Value?.ToString() ?? "",
                                            LevelName = worksheet.Cells[row, 9].Value?.ToString() ?? ""
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
                                        _Rerult.Add( new Question()
                                        {
                                            DateCreated = DateTime.UtcNow.AddHours(7),
                                            DateUpdated = DateTime.UtcNow.AddHours(7),
                                            DateDelete = DateTime.UtcNow.AddHours(7),
                                            CourseChapterId = CourseChapterID,
                                            UserCreated = AccountId,
                                            UserUpdated = AccountId,
                                            UserDelete = AccountId,
                                            QuestionContext = item.QuestionContext,
                                            AnswerId = _Answer.Where(a=>a.AnswerName ==item.Answer).FirstOrDefault()?.AnswerId??0,
                                            OptionA = item.OptionA,
                                            OptionB = item.OptionB,
                                            OptionC = item.OptionC,
                                            OptionD = item.OptionD,
                                            Solution = item.Solution,
                                            Image = item.UrlImage,
                                            Status = "Inactive",
                                            IsDelete = false,
                                            LevelId = _Levels.Where(a => a.LevelName == item.LevelName).FirstOrDefault()?.LevelId ?? 0,
                                        });
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

    }
}
