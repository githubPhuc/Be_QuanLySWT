using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;
using System.Collections;
using System.Data.Entity.Infrastructure;
//using be.Models;

namespace be.Repositories.ModRepository
{
    public class ModRepository : IModRepository
    {
        private readonly SwtDbContext _context;

        public ModRepository()
        {
            _context = new SwtDbContext();
        }
        public object AddMod(Account account)
        {
            try
            {
                account.Password = GenerateRandomString();
                _context.Add(account);
                _context.SaveChanges();
                return new
                {
                    message = "Add mod successfully",
                    account,
                    status = 200
                };
            } catch{
                return new
                {
                    message = "Add mod failly",
                    status = 400
                };
            }

        }

        public object ChangeStatus(int accountId, string status)
        {
            var updateStatus = _context.Accounts.SingleOrDefault(x => x.AccountId == accountId);
            if(updateStatus == null) 
            {
                return new
                {
                    message = "The user doesn't exist in database",
                    status = 400
                };
            } else
            {
                updateStatus.Status = status;
                _context.SaveChanges();
                return new
                {
                    status = 200,
                    updateStatus,
                    message = "Update successfully!"
                };
            }
        }

        public async Task<object> GetAllMod()
        {
            var data = (from account in _context.Accounts
                        where account.RoleId.Equals(3)
                        select new
                        {
                            account.AccountId,
                            account.Email,
                            account.Password,
                            account.CreateDate,
                            account.Status,
                            account.FullName,
                            account.BirthDay,
                            account.Phone,
                            account.Gender,
                            account.SchoolName,
                            account.Avatar,
                        }).OrderByDescending(x => x.AccountId).ToList();
            return new
            {
                status = 200,
                data,
            };
        }

        public object GetModById(int accountId)
        {
            var data = _context.Accounts.SingleOrDefault(x => x.AccountId == accountId);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        public object UpdateMod(Account account)
        {
            var updateMod = _context.Accounts.SingleOrDefault(x => x.AccountId == account.AccountId);
            updateMod.FullName = account.FullName;
            updateMod.Phone = account.Phone;
            updateMod.Gender = account.Gender;
            updateMod.BirthDay = account.BirthDay;
            try
            {
                _context.SaveChanges();
                return new
                {
                    message = "Update Ok",
                    status = 200,
                    data = updateMod
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                return new
                {
                    message = "Update failed",
                    status = 400,
                };
            }
        }

        public string GenerateRandomString()
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] randomChars = new char[10];
            randomChars[0] = characters[random.Next(26)];
            for (int i = 1; i < 10; i++)
            {
                randomChars[i] = characters[random.Next(characters.Length)];
            }
            string randomString = new string(randomChars);
            return randomString;
        }

        public IList<string> GetAllEmail()
        {
            var emailList = (from account in _context.Accounts
                            select account.Email).ToList();
            return emailList;
        }
    }
}
