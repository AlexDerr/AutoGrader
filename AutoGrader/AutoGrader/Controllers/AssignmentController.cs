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

            foreach(var test in testCases)
            {
                assignment.TestCases.Add(test);
            }

            return View(assignment);
        }

        public IActionResult SubmitAssignment(int Id)
        {
            SubmissionInputViewModel model = new SubmissionInputViewModel
            {
                AssignmentId = Id
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
                submission.AssignmentId = input.AssignmentId;

                SubmissionService submissionService = new SubmissionService(dbContext);
                submissionService.AddSubmission(submission);
                await dbContext.SaveChangesAsync(); // SubmissionId now set on local submission


                AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
                Assignment assignment = assignmentDataService.GetAssignmentById(submission.AssignmentId);

                assignment.Submissions.Add(submission);
                
                GraderMethod.GradeSubmission(submission, dbContext);

                if(submission.Compile())
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
