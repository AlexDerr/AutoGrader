using System.ComponentModel;

namespace AutoGrader.Models
{
    public class RegisterModel
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        [DisplayName("Student")]
        public bool IsStudent { get; set; }

        [DisplayName("Instructor")]
        public bool IsInstructor { get; set; }
    }
}