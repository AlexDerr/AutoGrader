using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Methods.Grader
{
    public class Grader
    {
        public void GradeSubmission(Assignment assignment, Submission submission)
        {
            SubmissionReport report = new SubmissionReport();

            report.UserId = submission.UserId;
            report.SubmissionId = submission.SubmissionId;
            // report.ReportId = next available id in database
            report.AssignmentId = submission.AssignmentId;
            report.SubmissionTime = submission.SubmissionTime;
            report.Language = submission.Language; // May need to change
            report.SourceCodeFileName = submission.SourceCodeFileName;


        }   
    }
}
