using AutoGrader.DataAccess;
using AutoGrader.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Data_Access.Services.UserDataService
{
    public class UserDataService : Service
    {
        public UserDataService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public Student GetUserByUsername(string userName)
        {
            return autoGraderDbContext.Students.First();
        }

        public void AddUser(User user)
        {
            if(user.Role == "Student")
            {
                Student student = new Student(user);
                autoGraderDbContext.Students.Add(student);
                autoGraderDbContext.SaveChanges();
            }
            else
            {
                Instructor instructor = new Instructor(user);
                autoGraderDbContext.Instructors.Add(instructor);
                autoGraderDbContext.SaveChanges();
            }
        }
    }
}
