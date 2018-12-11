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
using Microsoft.AspNetCore.Identity;

namespace AutoGrader.Controllers
{
    public class AssignmentController : Controller
    {
        private AutoGraderDbContext dbContext;

        public AssignmentController(AutoGraderDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                Name = assignment.Name,
                TestCase1Input = assignment.TestCases[0].Input,
                TestCase1Output = assignment.TestCases[0].ExpectedOutput,
                TestCase2Input = assignment.TestCases[1].Input,
                TestCase2Output = assignment.TestCases[1].ExpectedOutput
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

        public IActionResult SubmitAssignment(int Id, int studentId)
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
        public async Task<IActionResult> SubmitAssignment(SubmissionInputViewModel input)
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

                if (submission.Compile())
                {
                    submission.RunAndCompare();
                    submission.GradeTestCases();
                    submission.MaxRunTime();
                }

                dbContext.Submissions.Update(submission);
                dbContext.Assignments.Update(assignment);

                await dbContext.SaveChangesAsync();

                return RedirectToAction("SubmissionDetails", "Assignment", new { id = submission.SubmissionId });
            }

            return RedirectToAction("UnAuthorized", "Home");
        }

        public IActionResult AssignmentDetails(int id)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            Assignment assignment = assignmentDataService.GetAssignmentById(id);

            return View(assignment);
        }

        public IActionResult ViewAllSubmissions(int assignmentId, int studentId)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            IEnumerable<Submission> submissions = assignmentDataService.GetStudentSubmissionsOnAssignment(studentId, assignmentId).Reverse();
            var assignmentName = assignmentDataService.GetAssignmentById(assignmentId).Name;
            ViewData["AssignmentName"] = assignmentName;

            StudentDataService studentDataService = new StudentDataService(dbContext);
            var student = studentDataService.GetStudentById(studentId);
            var name = student.FirstName + " " + student.LastName;
            ViewData["StudentName"] = name;

            return View(submissions);
        }

        public IActionResult SubmissionDetails(int id)
        {
            SubmissionDataService submissionDataService = new SubmissionDataService(dbContext);
            var submission = submissionDataService.GetSubmissionById(id);

            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            ViewData["AssignmentName"] = assignmentDataService.GetAssignmentById(submission.AssignmentId).Name;

            return View(submission);
        }
    }
}
