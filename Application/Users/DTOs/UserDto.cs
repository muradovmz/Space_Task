namespace Application.Users.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string PrivateNumber { get; set; }
        public bool IsMarried { get; set; }
        public bool IsEmployed { get; set; }
        public decimal MonthSalary { get; set; }
    }
}