using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.DataAccess;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AutoGrader.Models;
using AutoGrader.Models.Users;
using AutoGrader.DataAccess.Services.ClassServices;
using AutoGrader.DataAccess.Services;
using AutoGrader.Models.Submission;

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

        public IActionResult EditClass(int id)
        {
            ClassDataService classDataService = new ClassDataService(dbContext);
            Class c = classDataService.GetClassById(id);

            EditClassViewModel model = new EditClassViewModel()
            {
                Name = c.Name,
                ClassId = c.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditClass(EditClassViewModel model)
        {
            ClassDataService classDataService = new ClassDataService(dbContext);
            classDataService.UpdateClass(model);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> DeleteClass(bool confirm, int classId)
        {
            ClassDataService classDataService = new ClassDataService(dbContext);

            classDataService.DeleteClass(classId);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
            ClassDataService classDataService = new ClassDataService(dbContext);
            Class c = classDataService.GetClassById(id);

            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            var list = assignmentDataService.GetAssignments().Where(e => e.ClassId == c.Id).Reverse();
            ViewData["Assignments"] = list;

            return View(c);
        }

        public IActionResult InstructorGradeByAssignment(int id)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            Assignment assignment = assignmentDataService.GetAssignmentById(id);

            ViewData["Title"] = "Grades: " + assignment.Name;
            ViewData["AssignmentName"] = assignment.Name;
            ViewData["AssignmentId"] = assignment.Id;

            ClassDataService classDataService = new ClassDataService(dbContext);
            Class c = classDataService.GetClassById(assignment.ClassId);


            StudentClassDataService studentDataService = new StudentClassDataService(dbContext);
            var students = studentDataService.GetStudentsByClassid(c.Id);

            foreach (var student in students)
            {
                var result = assignmentDataService.GetTopSubmissionForStudent(assignment.Id, student.Id);

                if (result == null)
                {
                    ViewData[student.FirstName + student.LastName] = 0.0;
                }
                else
                {
                    ViewData[student.FirstName + student.LastName] = result.Grade;
                }
            }

            return View(students);
        }

        public IActionResult InstructorAssignmentDetails(int id)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            var assignment = assignmentDataService.GetAssignmentById(id);

            return View(assignment);
        }

        public IActionResult AddExistingAssignment(int classId, int instructorId)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            var assignments = assignmentDataService.GetAssignmentsByInstructorId(instructorId).Reverse();

            ClassDataService classDataService = new ClassDataService(dbContext);
            var c = classDataService.GetClassById(classId);
            ViewData["ClassName"] = c.Name;
            ViewData["ClassId"] = c.Id;

            return View(assignments);
        }

        public async Task<IActionResult> AddSelectedAssignment(int classId, int assignmentId)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            var assignment = assignmentDataService.GetAssignmentById(assignmentId);

            var newAssignment = new Assignment
            {
                Submissions = new List<Submission>(),

                Name = assignment.Name,
                StartDate = assignment.StartDate,
                EndDate = assignment.EndDate,
                Description = assignment.Description,
                MemoryLimit = assignment.MemoryLimit,
                TimeLimit = assignment.TimeLimit,
                Languages = assignment.Languages,
                ClassId = classId,
            };

            assignmentDataService.AddAssignment(newAssignment);

            foreach (var test in assignment.TestCases)
            {
                var testCase = new TestCaseSpecification(test);
                testCase.AssignmentId = newAssignment.Id;

                newAssignment.TestCases.Add(testCase);
            }

            ClassDataService classDataService = new ClassDataService(dbContext);
            var c = classDataService.GetClassById(classId);
            c.Assignments.Add(newAssignment);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
