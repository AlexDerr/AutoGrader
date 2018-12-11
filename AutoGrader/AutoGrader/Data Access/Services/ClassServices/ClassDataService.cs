using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Users;
using AutoGrader.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            return autoGraderDbContext.Classes.Include(c => c.Assignments)
                .FirstOrDefault(e => e.Id == id);
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
        //Removed when added Join table StudentClasses
        /*
        public void AddStudent(Student student, Class c)
        {
            autoGraderDbContext.Classes.Update(c);
            c.Students.Add(student);
            autoGraderDbContext.SaveChanges();
        }
        */

        public void AddAssignment(Class classSection, Assignment assignment)
        {
            autoGraderDbContext.Classes.Update(classSection);
            classSection.Assignments.Add(assignment);
        }

        public void DeleteClass(int classId)
        {
            Class c = GetClassById(classId);

            autoGraderDbContext.Classes.Remove(c);

            IEnumerable<Assignment> assignments = autoGraderDbContext.Assignments.ToList().Where(e => e.ClassId == classId);
            foreach(var item in assignments)
            {
                foreach (var sub in item.Submissions)
                {
                    autoGraderDbContext.Submissions.Remove(sub);
                    autoGraderDbContext.SubmissionInputs.Remove(sub.Input);
                    autoGraderDbContext.SubmissionOutputs.Remove(sub.Output);
                    autoGraderDbContext.TestCaseReports.RemoveRange(sub.Output.TestCases);
                }
                autoGraderDbContext.Assignments.Remove(item);
                autoGraderDbContext.TestCaseSpecifications.RemoveRange(item.TestCases);
            }

            IEnumerable<StudentClass> studentClasses = autoGraderDbContext.StudentClasses.ToList().Where(e => e.ClassId == classId);
            foreach(var item in studentClasses)
            {
                autoGraderDbContext.StudentClasses.Remove(item);
            }
        }

        public void UpdateClass(EditClassViewModel model)
        {
            Class c = GetClassById(model.ClassId);
            c.Name = model.Name;
            autoGraderDbContext.Classes.Update(c);
        }
    }
}
