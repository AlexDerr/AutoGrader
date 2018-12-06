using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models;
using AutoGrader.Models.Users;

namespace AutoGrader.DataAccess.Services
{
    public class StudentClassDataService : Service
    {
        public StudentClassDataService(AutoGraderDbContext autoGraderDbContext) : base(autoGraderDbContext) { }


        public void AddStudentClass(Student student, Class c)
        {
            StudentClass sc = new StudentClass(student, c);

            autoGraderDbContext.StudentClasses.Add(sc);

            student.StudentClasses.Prepend(sc);
            c.StudentsEnrolled.Prepend(sc);

            autoGraderDbContext.Students.Update(student);
            autoGraderDbContext.Classes.Update(c);
        }

        public IEnumerable<Class> GetClassesByStudentId(int studentId)
        {
            var studentClasses = autoGraderDbContext.StudentClasses.Where(e => e.StudentId == studentId).ToList();
            List<Class> classes = new List<Class>();

            foreach (StudentClass student in studentClasses)
            {
                classes.Add(student.Class);
            }

            return classes.AsEnumerable();
        }
    }
}
