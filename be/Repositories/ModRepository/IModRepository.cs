using be.Models;
using Microsoft.AspNetCore.Mvc;
using be.DTOs;

namespace be.Repositories.ModRepository
{
    public interface IModRepository
    {
        Task<object> GetAllMod();
        object GetModById(int accountId);
        object AddMod(Account account);
        object ChangeStatus(int accountId, string status);
        object UpdateMod(Account account);
        IList<string> GetAllEmail();

    }
}
