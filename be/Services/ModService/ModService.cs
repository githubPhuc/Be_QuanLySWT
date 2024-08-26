using be.DTOs;
using be.Models;
using be.Repositories.ModRepository;

namespace be.Services.ModService
{
    public class ModService : IModService
    {
        private readonly IModRepository _modRepository;

        public ModService()
        {
            _modRepository = new ModRepository();
        }
        public object AddMod(Account account)
        {
            var result = _modRepository.AddMod(account);
            return result;
        }

        public object ChangeStatus(int accountId, string status)
        {
            return _modRepository.ChangeStatus(accountId, status);
        }

        public IList<string> GetAllEmail()
        {
            return _modRepository.GetAllEmail();
        }

        public async Task<object> GetAllMod()
        {
            return await _modRepository.GetAllMod();
        }

        public object GetModById(int accountId)
        {
            return _modRepository.GetModById(accountId);
        }

        public object UpdateMod(Account account)
        {
            return _modRepository.UpdateMod(account);
        }
    }
}
