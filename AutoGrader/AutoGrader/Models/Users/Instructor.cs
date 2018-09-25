using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Users
{
    public class Instructor : User
    {
        public Instructor() { }

        public Instructor(RegisterViewModel model)
        {
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.Username = model.Username;
            this.Email = model.Email;
            this.IsInstructor = model.IsInstructor;
            this.IsStudent = model.IsStudent;
            this.Password = model.Password;
        }

        public List<Class> Classes { get; set; }
        public int Id { get; set; }
    }
}
