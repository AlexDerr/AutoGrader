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

        public IActionResult SubmitAssignment(int Id)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);

            Assignment assignment = assignmentDataService.GetAssignmentById(Id);

            SubmissionInputViewModel model = new SubmissionInputViewModel
            {
                AssignmentId = Id,
                Language = assignment.Languages[0]
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

                SubmissionService submissionService = new SubmissionService(dbContext);
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

        public IActionResult ViewSubmissions(int assignmentId, int studentId)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            var submissions = assignmentDataService.GetStudentSubmissionsOnAssignment(studentId, assignmentId);
            var assignmentName = assignmentDataService.GetAssignmentById(assignmentId).Name;
            ViewData["AssignmentName"] = assignmentName;

            return View(submissions);
        }
    }
}
