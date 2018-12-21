using System.Threading.Tasks;
using AutoGrader.DataAccess;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;
using AutoGrader.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ShellHelper;
using AutoGrader.Methods.GraderMethod;
using AutoGrader.Models;
using AutoGrader.DataAccess.Services.ClassServices;
using System.Collections.Generic;
using System;
using System.Linq;
using AutoGrader.Models.Users;
using AutoGrader.DataAccess.Services;
using Microsoft.AspNetCore.Identity;

namespace AutoGrader.Controllers
{
    public class AssignmentController : Controller
    {
        private AutoGraderDbContext dbContext;
        private UserManager<IdentityUser> UserManager;
        private Student student;

        public AssignmentController(AutoGraderDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.UserManager = userManager;
        }

        public IActionResult CreateAssignment(int Id)
        {
            AssignmentViewModel model = new AssignmentViewModel()
            {
                ClassId = Id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment(AssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Assignment assignment = new Assignment(model);
                AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
                assignmentDataService.AddAssignment(assignment);

                ClassDataService classDataService = new ClassDataService(dbContext);
                Class c = classDataService.GetClassById(assignment.ClassId);
                c.Assignments.Add(assignment);
            }

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
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

        public IActionResult InstructorAssignmentDetails(int id)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            var assignment = assignmentDataService.GetAssignmentById(id);

            return View(assignment);
        }

        public IActionResult StudentAssignmentDetails(int id)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            Assignment assignment = assignmentDataService.GetAssignmentById(id);

            return View(assignment);
        }

        public IActionResult EditAssignment(int id)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            Assignment assignment = assignmentDataService.GetAssignmentById(id);

            EditAssignmentViewModel model = new EditAssignmentViewModel()
            {
                ClassId = assignment.ClassId,
                Description = assignment.Description,
                EndDate = assignment.EndDate,
                StartDate = assignment.StartDate,
                Languages = assignment.Languages,
                MemoryLimit = assignment.MemoryLimit,
                TimeLimit = assignment.TimeLimit,
                Name = assignment.Name,
                NumberOfTestCases = assignment.TestCases.Count,
                TestCases = assignment.TestCases
            };

            ViewData["AssignmentId"] = assignment.Id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAssignment(EditAssignmentViewModel model, int assignmentId)
        {

            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            Assignment assignment = assignmentDataService.GetAssignmentById(assignmentId);
            assignmentDataService.UpdateAssignment(model, assignment);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Instructor", new { id = assignment.ClassId});
        }

        public async Task<ActionResult> DeleteAssignment(int assignmentId)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            Assignment assignment = assignmentDataService.GetAssignmentById(assignmentId);

            assignmentDataService.DeleteAssignment(assignment);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Instructor", new { id = assignment.ClassId });
        }

        public IActionResult ViewAssignments(int classId)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            IEnumerable<Assignment> assignments = assignmentDataService.GetAssignmentsByClassId(classId).Reverse();

            StudentDataService studentDataService = new StudentDataService(dbContext);
            student = studentDataService.GetStudentByUsername(UserManager.GetUserName(User));

            ViewData.Add("StudentId", student.Id);

            return View(assignments);
        }

        public IActionResult SubmitToAssignment(int Id, int studentId)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);

            Assignment assignment = assignmentDataService.GetAssignmentById(Id);

            SubmissionInputViewModel model = new SubmissionInputViewModel
            {
                AssignmentId = Id,
                Language = assignment.Languages[0],
                UserId = studentId,
                ClassId = assignment.ClassId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitToAssignment(SubmissionInputViewModel input)
        {
            StudentDataService studentDataService = new StudentDataService(dbContext);
            Student student = studentDataService.GetStudentById(input.UserId);

            bool hasAccess = false;

            foreach(StudentClass studentClass in student.StudentClasses)
            {
                if(studentClass.ClassId == input.ClassId)
                {
                    hasAccess = true;
                }
            }

            if (ModelState.IsValid && hasAccess)
            {
                Submission submission = new Submission();

                submission.Input.SourceCode = input.SourceCode;
                submission.Input.Language = input.Language;
                submission.AssignmentId = input.AssignmentId;
                submission.UserId = input.UserId;
                submission.SubmissionTime = DateTime.Now;

                SubmissionDataService submissionService = new SubmissionDataService(dbContext);
                submissionService.AddSubmission(submission);
                await dbContext.SaveChangesAsync(); // SubmissionId now set on local submission


                AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
                Assignment assignment = assignmentDataService.GetAssignmentById(submission.AssignmentId);

                assignment.Submissions.Add(submission);

                GraderMethod.GradeSubmission(submission, dbContext);
                submission.Output.Runtime = assignment.TimeLimit;

                if (submission.Compile())
                {
                    submission.RunAndCompare();
                    submission.GradeTestCases();
                    submission.MaxRunTime();
                }

                dbContext.Submissions.Update(submission);
                dbContext.Assignments.Update(assignment);

                await dbContext.SaveChangesAsync();

                submission.deleteJunkFiles();

                return RedirectToAction("SubmissionDetails", "Submission", new { id = submission.SubmissionId });
            }

            return RedirectToAction("UnAuthorized", "Home");
        }

        public IActionResult GradesByAssignment(int id)
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
    }
}
