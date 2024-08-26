using be.DTOs;
using be.Models;
using be.Repositories.NewFolder;
using Microsoft.Identity.Client;
using System.Collections;
using System.Data.Entity.Infrastructure;

namespace be.Repositories.SuperAdminRepository
{
    public class SuperAdminRepository : ISuperAdminRepository
    {
        private readonly SwtDbContext _context;

        public SuperAdminRepository()
        {
            _context = new SwtDbContext();
        }
        public object AddAdmin(SuperAdminDTO adminDTO)
        {
            var addAdmin = new Account();
            addAdmin.Email = adminDTO.Email;
            addAdmin.Phone = adminDTO.Phone;    
            addAdmin.Password = adminDTO.Password;
            addAdmin.FullName = adminDTO.FullName;
            addAdmin.CreateDate = DateTime.Now;
            addAdmin.Status = "Đang hoạt động";
            addAdmin.RoleId = 2;
            _context.Accounts.Add(addAdmin);
            try
            {
                _context.SaveChanges();
                return new
                {
                    message = "Add success",
                    status = 200,
                    data = addAdmin,
                };
            } catch
            {
                return new
                {
                    message = "Failed",
                    status = 400,
                };
            }
        }

        public object ChangeStatus(int accountId, string status)
        {
            var updateAdmin = _context.Accounts.SingleOrDefault(x => x.AccountId == accountId);
            if(updateAdmin == null)
            {
                return new
                {
                    message = "The user do not exist in database",
                    status = 400,
                };
            }
            updateAdmin.Status = status;
            _context.SaveChanges();
            return new
            {
                status = 200,
                data = updateAdmin,
                message = "Update success",
            };
        }

        public object GetAdminById(int accountId)
        {
            throw new NotImplementedException();
        }

        public object GetAllAdmin()
        {
            var result = _context.Accounts.Where(x => x.RoleId == 2).OrderByDescending(x => x.AccountId);
            if (result == null)
            {
                return new
                {
                    message = "No data",
                    status = 400,
                };
            }
            return new
            {
                message = "Get Data success",
                status = 200,
                data = result,
            };
        }

        public object GetAllEmail()
        {
            List<string> strings = new List<string>();
            var emailList = from email in _context.Accounts
                            select new
                            {
                                email.Email,
                            };
            foreach ( var email in emailList.ToList())
            {
                strings.Add(email.Email);
            }
            return new
            {
                message = "Get Data",
                status = 200,
                data = strings.ToList(),
            };
        }

        public object UpdateAdmin(UpdateAdminSuperDTO updateAdmin)
        {
            try
            {
                var admin = _context.Accounts.SingleOrDefault(x => x.AccountId == updateAdmin.AccountId);
                admin.FullName = updateAdmin.FullName;
                admin.Password = updateAdmin.Password;
                admin.Phone = updateAdmin.Phone;
                _context.SaveChanges();
                return new
                {
                    message = "Update successfully",
                    status = 200,
                    data = admin,
                };
            }
            catch
            {
                return new
                {
                    message = "Failed",
                    status = 400,
                };
            }


        }
    }
}
