using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models;
using AutoGrader.Models.Users;

namespace AutoGrader.DataAccess.Services.ClassServices
{
    public class ClassDataService : Service
    {
        public ClassDataService(AutoGraderDbContext dbContext) : base(dbContext){}

        public IEnumerable<Class> GetClasses()
        {
            return autoGraderDbContext.Classes.ToList();
        }

        public Class GetClassById(int id)
        {
            return GetClasses().FirstOrDefault(e => e.Id == id);
        }

        public Class GetClassByKey(string key)
        {
            return autoGraderDbContext.Classes.FirstOrDefault(e => e.ClassKey == key);
        }

        public void AddClass(Class classSection)
        {
            autoGraderDbContext.Classes.Add(classSection);
        }

        public IEnumerable<Class> GetClassesByInstuctorId(int id)
        {
            return GetClasses().Where(e => e.InstructorId == id);
        }

        public void AddStudent(Student student, Class c)
        {
            autoGraderDbContext.Classes.Update(c);
            c.Students.Add(student);
            autoGraderDbContext.SaveChanges();
        }
    }
}
