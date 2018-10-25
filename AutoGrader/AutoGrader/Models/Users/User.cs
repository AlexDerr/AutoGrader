using Microsoft.AspNetCore.Identity;

namespace AutoGrader.Models.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsStudent { get; set; }
        public bool IsInstructor { get; set; }
    }
}