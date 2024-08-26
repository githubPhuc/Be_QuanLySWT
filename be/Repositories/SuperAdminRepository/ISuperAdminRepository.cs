using be.Models;
using Microsoft.AspNetCore.Mvc;
using be.DTOs;

namespace be.Repositories.NewFolder
{
    public interface ISuperAdminRepository
    {
        object GetAllAdmin();
        object GetAdminById(int accountId);
        object AddAdmin(SuperAdminDTO adminDTO);
        object ChangeStatus(int accountId, string status);
        object UpdateAdmin(UpdateAdminSuperDTO updateAdmin);
        object GetAllEmail();
    }
}
