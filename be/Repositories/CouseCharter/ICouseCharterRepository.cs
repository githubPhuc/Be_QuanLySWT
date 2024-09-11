using be.Models;
using Microsoft.AspNetCore.Mvc;
using be.DTOs;
using be.Repositories.CouseCharter;

namespace be.Repositories.ModRepository
{
    public interface ICouseCharterRepository
    {
        public Task<List<getCouseCharter>> GetAllListCouseCharter(string? ChapterTitleSearch);
        public Task<List<getQuestionInCourseChapter>> GetAllListQuestionInCouseCharter(int IdCouseChapter, string? nameQuestion);
        public Task<List<getCouseCharter>> GetCouseCharterByGrade(int GradeId, int SubjectId, string? ChapterSearch);
        public Task<List<getQuestionInCourseChapter>> GetQuestionByCourseChaptersInUser(int IdCourseChapter);
        public Task<string> AddCourceCharter(PostDataInsertCourseChapter model);
        public Task<string> UpdateCourceCharter(int IdCourceChapter, PostDataInsertCourseChapter model);
        public Task<string> DeleteCourceCharter(int IdCourceChapter, int AccountId);
        public Task<string> ConfirmCourceCharter(int IdCourceChapter, int AccountId);
        public Task<string> AddQuestionInCourseChapterID(PostDataInsertQuestionInCourseChapterID model);
        public Task<string> UpdateQuestionInCourseChapterID(PostDataInsertQuestionInCourseChapterID model, int QuestionId);
        public Task<string> DeleteQuestionInCourseChapterID(int QuestionId, int AccountId);
    }
}
