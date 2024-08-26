using System.Security.Policy;

namespace be.DTOs
{
    public class SuperAdminDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }

    }

    public class UpdateAdminSuperDTO
    {
        public int? AccountId { get; set; }
        public string? Email { get; set; }   
        public string? Password { get; set; }    
        public string? FullName { get; set; }    
        public string? Phone { get; set; }   
    }
}
