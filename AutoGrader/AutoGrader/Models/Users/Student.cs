using System.Collections.Generic;
using AutoGrader.Models;

namespace AutoGrader.Models.Users
{
    public class Student : User
    {
        public Student()
        {
            StudentClasses = new List<StudentClass>();
        }
        public Student(User model)
        {
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.UserName = model.UserName;
            this.Email = model.Email;
            this.Role = model.Role;
            StudentClasses = new List<StudentClass>();
        }

        public IEnumerable<StudentClass> StudentClasses { get; set; }
        public int Id { get; set; }
    }
}