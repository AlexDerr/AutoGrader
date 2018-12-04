using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AutoGrader.Areas.Identity.Pages.Account.RegisterModel;

namespace AutoGrader.Models.Users
{
    public class Student : User
    {
        public Student()
        {
            Classes = new List<Class>();
        }
        public Student(User model)
        {
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.UserName = model.UserName;
            this.Email = model.Email;
            this.Role = model.Role;
            Classes = new List<Class>();
        }

        public List<Class> Classes { get; set; }
        public int Id { get; set; }
    }
}