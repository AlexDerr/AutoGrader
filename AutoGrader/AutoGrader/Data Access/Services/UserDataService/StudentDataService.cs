using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Users;

namespace AutoGrader.DataAccess
{
    public class StudentDataService : Service
    {
        public StudentDataService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<Student> GetStudents()
        {
            return autoGraderDbContext.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            return GetStudents().FirstOrDefault(e => e.Id == id);
        }

        public void AddStudent(Student student)
        {
            autoGraderDbContext.Students.Add(student);
        }
    }
}
