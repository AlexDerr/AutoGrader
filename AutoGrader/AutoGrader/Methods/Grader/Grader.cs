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
        public void GradeSubmission(Assignment assignment, SubmissionInput submission)
        {
            SubmissionOutput report = new SubmissionOutput();

            report.UserId = submission.UserId;
            report.SubmissionInputId = submission.SubmissionInputId;
            // report.ReportId = next available id in database
            report.AssignmentId = submission.AssignmentId;
            report.SubmissionTime = submission.SubmissionTime;
            report.Language = submission.Language; // May need to change depending how we represent language
            report.SourceCodeFileName = submission.SourceCodeFileName;

            CompileCheckInput compileInput = new CompileCheckInput();
            compileInput.SubmissionId = report.SubmissionInputId;
            // Send the CompileCheckInput to the compiler and get the output back
            // Object returned with contain a pass or fail bool, and compiler output
            
            // if it did not compile correctly, make the output the error message
            //      Print out compiler output
            //      Mark all test cases on the report as failed
            // else
            //      call the grading function passing an object that contains ** 2d array of input output files/or assignment ID **, source code, language, runtime, and submission id
            //      get back a list of objects in the same amount as the num of test cases, that each contain pass/fail bool, compiler/runner output, and runtime 
            //      Update the database for the report ID to say which test cases were passed and save the output string for each

        }
    }
}