using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AutoGrader.Models;
using Microsoft.AspNetCore.Http;
using AutoGrader.Models.Submission;
using AutoGrader.Models.ViewModels;
using System;
using ShellHelper;

namespace AutoGrader.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult SubmitAssignment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitAssignment(SubmissionInputViewModel input)
        {
            Submission submission = new Submission();

            submission.Input.SourceCode = input.SourceCode;
            submission.Input.Language = Language.Cpp;

            if (submission.Compile())
            {
                submission.Run(0);
                return View("Index");
            }
            else
            {
                submission.Output.Compiled = false;
            }


            //Todo - add other fields to submission, save to DB, other business logic

            return View("Index");
        }

        public IActionResult CreateAssignment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAssignment(AssignmentViewModel assignment)
        {
            //Todo - create the assignment and save it in the db

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
