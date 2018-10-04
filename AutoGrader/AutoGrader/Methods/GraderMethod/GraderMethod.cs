using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;
using ShellHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Methods.GraderMethod
{
    public class GraderMethod
    {
        public void GradeSubmission(Submission submission)
        {
            //if (submission.Compile())
            //{
                
            //}
            // Send the Submission to the compiler and get the output back
            // Object returned with contain a pass or fail bool, and compiler output
            
            // if it did not compile correctly, make the output the error message
            //      Print out compiler output
            //      Mark all test cases on the report as failed in submissionOutput
            // else
            //      call the grading function passing the submission, ** 2d array of input output files/or assignment ID **, source code, language, runtime, and submission id
            //      get back the submission with the chekced fields updated
            
            // Update the database for the report ID to say which test cases were passed and save the output string for each
            // return submission / submission output to the user
        }

        //public void TimedRun(this Submission obj)
        //{

        //}
    }
}