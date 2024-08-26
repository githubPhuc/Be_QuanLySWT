using be.DTOs;
using be.Models;

namespace be.Services.SuperAdminService
{
    public interface ISuperAdminService
    {
        object GetAllAdmin();
        object GetAdminById(int accountId);
        object AddAdmin(SuperAdminDTO adminDTO);
        object ChangeStatus(int accountId, string status);
        object UpdateAdmin(UpdateAdminSuperDTO updateAdmin);
        object GetAllEmail();
    }
}
