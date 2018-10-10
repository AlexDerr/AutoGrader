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

namespace AutoGrader.Controllers
{
    public class AssignmentController : Controller
    {
        private AutoGraderDbContext dbContext;

        public AssignmentController(AutoGraderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult CreateAssignment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment(AssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Assignment assignment = new Assignment(model);
                AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
                assignmentDataService.AddAssignment(assignment);
            }

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult SubmitAssignment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAssignment(SubmissionInputViewModel input)
        {
            if (ModelState.IsValid)
            {
                Submission submission = new Submission();

                submission.Input.SourceCode = input.SourceCode;
                submission.Input.Language = Language.Cpp;

                SubmissionService submissionService = new SubmissionService(dbContext);
                submissionService.AddSubmission(submission);

                if (submission.Compile())
                {
                    for (int i = 0; i < submission.Output.TestCases.Count; i++)
                    {
                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        submission.Run(i);
                        watch.Stop();
                        if (submission.Output.Runtime < (double)watch.ElapsedMilliseconds)
                        {
                            submission.Output.Runtime = (double)watch.ElapsedMilliseconds;
                        }
                    }
                }
                else
                {
                    submission.Output.Compiled = false;
                }
            }

            await dbContext.SaveChangesAsync();

            return View("SubmitAssignment");
        }
    }
}
