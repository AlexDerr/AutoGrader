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

        public User GetUserByUsername(string userName)
        {
            User student = autoGraderDbContext.Students.FirstOrDefault(e => e.UserName == userName);
            if(student != null)
                return student;
            else
            {
                User instructor = autoGraderDbContext.Instructors.FirstOrDefault(e => e.UserName == userName);
                return instructor;
            }
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
