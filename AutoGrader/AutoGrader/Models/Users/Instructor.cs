using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Users
{
    public class Instructor : User
    {
        public Instructor() { }

        public Instructor(User model)
        {
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.UserName = model.UserName;
            this.Email = model.Email;
            this.Role = model.Role;
        }

        public List<Class> Classes { get; set; }
        public int Id { get; set; }
    }
}
