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

            IEnumerable<TestCaseSpecification> testCases = assignmentDataService.GetTestCases(assignment.Id);

            AssignmentViewModel model = new AssignmentViewModel();

            ViewData["AssignmentId"] = assignment.Id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAssignment(AssignmentViewModel model, int assignmentId)
        {

            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            Assignment assignment = assignmentDataService.GetAssignmentById(assignmentId);
            //assignmentDataService.UpdateAssignment(assignment, model);

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
                UserId = studentId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAssignment(SubmissionInputViewModel input)
        {
            if (ModelState.IsValid)
            {
                Submission submission = new Submission();

                submission.Input.SourceCode = input.SourceCode;
                submission.Input.Language = input.Language;
                submission.AssignmentId = input.AssignmentId;
                submission.UserId = input.UserId;

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
                }

                dbContext.Assignments.Update(assignment);
            }

            await dbContext.SaveChangesAsync();

            return RedirectToAction("StudentHome", "Student");
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
            var submissions = assignmentDataService.GetStudentSubmissionsOnAssignment(studentId, assignmentId);
            var assignmentName = assignmentDataService.GetAssignmentById(assignmentId).Name;
            ViewData["AssignmentName"] = assignmentName;

            StudentDataService studentDataService = new StudentDataService(dbContext);
            var student = studentDataService.GetStudentById(studentId);
            var name = student.FirstName + " " + student.LastName;
            ViewData["StudentName"] = name;

            // TEST:
            //Submission temp = new Submission()
            //{
            //    SubmissionId = 6969,
            //    SubmissionTime = DateTime.Now,
            //    Grade = 50,
            //};

            //submissions.Add(temp);

            return View(submissions);
        }

        public IActionResult SubmissionDetails(int id)
        {
            SubmissionDataService submissionDataService = new SubmissionDataService(dbContext);
            var submission = submissionDataService.GetSubmissionById(id);

            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            ViewData["AssignmentName"] = assignmentDataService.GetAssignmentById(submission.AssignmentId).Name;

            // TEST:
            //    ViewData["AssignmentName"] = "This dumb shit assignment";
            //    Submission temp = new Submission()
            //    {
            //        UserId = 0,
            //        SubmissionId = 6969,
            //        SubmissionTime = DateTime.Now,
            //        Grade = 50,
            //    };

            //    temp.Input.SourceCode = "int main()\n{\n\n}";
            //    temp.Input.Language = "C++";

            //    TestCaseReport test = new TestCaseReport();

            //    test.Pass = false;

            //    temp.Output.TestCases.Add(test);
            //    temp.Output.TestCases.Add(test);

            //    return View(temp);
            //}
            return View(submission);
        }
    }
}
