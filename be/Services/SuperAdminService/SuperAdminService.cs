using be.DTOs;
using be.Repositories.NewFolder;
using be.Repositories.NewsRepository;
using be.Repositories.SuperAdminRepository;

namespace be.Services.SuperAdminService
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ISuperAdminRepository _superAdminRepository;
        public SuperAdminService()
        {
            _superAdminRepository = new SuperAdminRepository();
        }
        public object AddAdmin(SuperAdminDTO adminDTO)
        {
            return _superAdminRepository.AddAdmin(adminDTO);
        }

        public object ChangeStatus(int accountId, string status)
        {
            return _superAdminRepository.ChangeStatus(accountId, status);   
        }

        public object GetAdminById(int accountId)
        {
            return _superAdminRepository.GetAdminById(accountId);
        }

        public object GetAllAdmin()
        {
            return _superAdminRepository.GetAllAdmin();
        }

        public object GetAllEmail()
        {
            return _superAdminRepository.GetAllEmail();
        }

        public object UpdateAdmin(UpdateAdminSuperDTO updateAdmin)
        {
            return _superAdminRepository.UpdateAdmin(updateAdmin);
        }
    }
}
