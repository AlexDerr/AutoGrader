using Microsoft.AspNetCore.Identity;
using static AutoGrader.Areas.Identity.Pages.Account.RegisterModel;

namespace AutoGrader.Models.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}