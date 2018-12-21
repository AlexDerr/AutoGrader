using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.DataAccess;
using AutoGrader.Models.Submission;
using Microsoft.AspNetCore.Mvc;

namespace AutoGrader.Controllers
{
    public class SubmissionController : Controller
    {
        private AutoGraderDbContext dbContext;

        public SubmissionController(AutoGraderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult ViewAllSubmissionsOnAssignment(int assignmentId, int studentId)
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