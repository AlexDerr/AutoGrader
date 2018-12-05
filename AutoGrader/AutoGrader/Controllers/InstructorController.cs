using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.DataAccess;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;
using AutoGrader.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ShellHelper;
using AutoGrader.Methods.GraderMethod;
using AutoGrader.Models;
using AutoGrader.Models.Users;
using AutoGrader.DataAccess.Services.ClassServices;
using AutoGrader.Methods.ClassMethod;

namespace AutoGrader.Controllers
{
    public class InstructorController : Controller
    {
        private AutoGraderDbContext dbContext;

        public InstructorController(AutoGraderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult InstructorHome(User user)
        {
            ClassDataService classDataService = new ClassDataService(dbContext);
            IEnumerable<Class> classes = classDataService.GetClassesByInstuctorId(Convert.ToInt32(user.Id));

            ViewData.Add("InstructorId", user.Id);

            return View(classes);
        }

        public IActionResult CreateClass(int id)
        {
            ClassViewModel model = new ClassViewModel()
            {
                InstructorId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(ClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                Class thisClass = new Class(model);
                ClassDataService classDataService = new ClassDataService(dbContext);
                classDataService.AddClass(thisClass);
            }

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
            ClassDataService classDataService = new ClassDataService(dbContext);
            Class c = classDataService.GetClassById(id);

            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            var list = assignmentDataService.GetAssignments().Where(e => e.ClassId == c.Id);
            ViewData["Assignments"] = list;

            return View(c);
        }

        public IActionResult InstructorGradeByAssignment(int id)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            Assignment assignment = assignmentDataService.GetAssignmentById(id);

            ViewData["Title"] = "Grades: " + assignment.Name;
            ViewData["AssignmentName"] = assignment.Name;

            ClassDataService classDatService = new ClassDataService(dbContext);
            Class c = classDatService.GetClassById(assignment.ClassId);

            var students = c.StudentsEnrolled;

            foreach (var student in students)
            {
                ViewData[student.Student.FirstName + student.Student.LastName] = assignmentDataService.GetTopSubmissionForStudent(assignment.Id, student.Student.Id).Grade;
            }

            return View(students);
        }

        //public IActionResult InstructorGrade(User user)
        //{
        // 
        //    //return View(students);
        //}
    }
}
