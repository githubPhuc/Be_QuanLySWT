using be.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace be.Repositories.Graden
{
    public class GradenRepository : IGradeRepository
    {
        private readonly SwtDbContext _context;
        public GradenRepository(SwtDbContext context)
        {
            this._context = context;
        }
        public async Task<List<GetListGrade>> GetListGrade(string? NameSearch)
        {
            try
            {
                var data = await _context.Grades
                    .Where(a => a.IsDelete == false &&
                           (string.IsNullOrEmpty(NameSearch) == true ||
                           a.NameGrade.ToLower().Contains(NameSearch.ToLower())))
                    .Select(a => new GetListGrade
                    {
                        GradeId = a.GradeId,
                        DateCreated = a.DateCreated,
                        DateDelete = a.DateDelete,
                        DateUpdated = a.DateUpdated,
                        IsDelete = a.IsDelete,
                        NameGrade = a.NameGrade,
                        Status = a.Status,
                        UserCreated = a.UserCreated,
                        UserDelete = a.UserDelete,
                        UserUpdated = a.UserUpdated,
                    })
                    .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> CreateGrade(string NameGrade, int AccountId)
        {
            try
            {
                if (string.IsNullOrEmpty(NameGrade))
                {
                    throw new Exception("Name grade is null");
                }
                if (AccountId==0)
                {
                    throw new Exception("Account is null");
                }
                var data = await _context.Grades
                    .Where(a=>a.NameGrade == NameGrade)
                    .FirstOrDefaultAsync();
                if(data == null)
                {
                    throw new Exception("data already exists");
                }
                else
                {
                    var result = new Grade()
                    {
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        DateUpdated = DateTime.UtcNow.AddHours(7),
                        DateDelete = DateTime.UtcNow.AddHours(7),
                        UserUpdated = AccountId,
                        UserCreated = AccountId,
                        UserDelete = AccountId,
                        Status = "Active",
                        IsDelete = false,
                        NameGrade = NameGrade,
                    };
                    _context.Grades.Add(result);
                    await _context.SaveChangesAsync();
                    return "Create success";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> UpdateGrade(int GradeId, string NameGrade, int AccountId)
        {
            try
            {
                if (string.IsNullOrEmpty(NameGrade))
                {
                    throw new Exception("Name grade is null");
                }
                if (AccountId == 0)
                {
                    throw new Exception("Account is null");
                }
                var data = await _context.Grades
                    .Where(a => a.GradeId == GradeId)
                    .FirstOrDefaultAsync();
                if (data == null)
                {
                    throw new Exception("data already exists");
                }
                else
                {
                    data.DateUpdated = DateTime.UtcNow.AddHours(7);
                    data.UserUpdated = AccountId;
                    data.NameGrade = NameGrade;
                    _context.Grades.Update(data);
                    await _context.SaveChangesAsync();
                }

                return "Update success";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> DeleteGrade(int GradeId,  int AccountId)
        {
            try
            {
                
                if (AccountId == 0)
                {
                    throw new Exception("Account is null");
                }
                var data = await _context.Grades
                    .Where(a => a.GradeId == GradeId)
                    .FirstOrDefaultAsync();
                if (data == null)
                {
                    throw new Exception("data already exists");
                }
                else
                {

                    data.DateDelete = DateTime.UtcNow.AddHours(7);
                        data.UserDelete = AccountId;
                        data.IsDelete = true;

                    _context.Grades.Update(data);
                    await _context.SaveChangesAsync();
                }

                return "Delete success";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
