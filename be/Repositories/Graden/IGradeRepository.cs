using be.Models;

namespace be.Repositories.Graden
{
    public interface IGradeRepository
    {
        public Task<List<GetListGrade>> GetListGrade(string? NameSearch);
        public Task<string> CreateGrade(string NameGrade, int AccountId);
        public Task<string> UpdateGrade(int GradeId, string NameGrade, int AccountId);
        public Task<string> DeleteGrade(int GradeId, int AccountId);
    }
}
