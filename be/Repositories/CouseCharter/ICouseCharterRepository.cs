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
    }
}
