using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser:IdentityUser
    {
        public string PrivateNumber { get; set; }
        public bool IsMarried { get; set; }
        public bool IsEmployed { get; set; }
        public decimal MonthSalary { get; set; }
        public Address Address { get; set; }
    }
}