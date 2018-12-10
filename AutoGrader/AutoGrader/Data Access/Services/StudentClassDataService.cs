using System.Collections.Generic;
using System.Linq;
using AutoGrader.DataAccess.Services.ClassServices;
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

            student.StudentClasses.Add(sc);
            c.StudentsEnrolled.Add(sc);

            autoGraderDbContext.Students.Update(student);
            autoGraderDbContext.Classes.Update(c);
        }

        public IEnumerable<Class> GetClassesByStudentId(int studentId)
        {
            var studentClasses = autoGraderDbContext.StudentClasses.Where(e => e.StudentId == studentId).ToList();
            List<Class> classes = new List<Class>();

            ClassDataService classDataService = new ClassDataService(autoGraderDbContext);

            foreach (var item in studentClasses)
            {
                item.Class = classDataService.GetClassById(item.ClassId);
                classes.Add(item.Class);
            }

            return classes;
        }

        public void RemoveStudentClass(Student student, Class c)
        {
            StudentClass sc = autoGraderDbContext.StudentClasses.FirstOrDefault(e => e.StudentId == student.Id && e.ClassId == c.Id);
            autoGraderDbContext.StudentClasses.Remove(sc);

            c.StudentsEnrolled.Remove(sc);
            student.StudentClasses.Remove(sc);

            foreach(var item in c.Assignments)
            {
                autoGraderDbContext.Submissions.RemoveRange(item.Submissions.ToList().Where(e => e.UserId == student.Id));
                foreach(var sub in item.Submissions)
                {
                    autoGraderDbContext.TestCaseReports.RemoveRange(sub.Output.TestCases);
                }
                autoGraderDbContext.Assignments.Update(item);
            }

            autoGraderDbContext.Students.Update(student);
            autoGraderDbContext.Classes.Update(c);
        }

        public IEnumerable<Student> GetStudentsByClassid(int classId)
        {
            var list = autoGraderDbContext.StudentClasses.ToList().Where(e => e.ClassId == classId);

            var students = new List<Student>();

            foreach (var item in list)
            {
                students.Add(autoGraderDbContext.Students.FirstOrDefault(e => e.Id == item.StudentId));
            }

            return students;
        }
    }
}