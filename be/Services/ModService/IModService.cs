using be.DTOs;
using be.Models;

namespace be.Services.ModService
{
    public interface IModService
    {
        Task<object> GetAllMod();
        object GetModById(int accountId);
        object AddMod(Account account);
        object ChangeStatus(int accountId, string status);
        object UpdateMod(Account account);
        IList<string> GetAllEmail();
    }
}
