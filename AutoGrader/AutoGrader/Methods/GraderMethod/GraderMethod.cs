﻿using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;
using ShellHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.DataAccess;

namespace AutoGrader.Methods.GraderMethod
{
    public static class GraderMethod
    {
        public static void GradeSubmission(Submission submission, AutoGraderDbContext dbContext)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            Assignment assignment = assignmentDataService.GetAssignmentById(submission.AssignmentId);
            submission.Output.MemoryLimit = assignment.MemoryLimit;

            TestCaseDataService testCaseDataService = new TestCaseDataService(dbContext);
            IEnumerable<TestCaseSpecification> testCaseSpecifications = testCaseDataService.GetTestCaseByAssignmentId(submission.AssignmentId);
            foreach (TestCaseSpecification testCaseSpecification in testCaseSpecifications)
            {
                TestCaseReport testCaseReport = new TestCaseReport();
                testCaseReport.CodeInput = testCaseSpecification.Input;
                testCaseReport.ExpectedOutput = testCaseSpecification.ExpectedOutput;
                testCaseReport.Feedback = testCaseSpecification.Feedback;
                submission.Output.TestCases.Add(testCaseReport);
            }

/*             if (submission.Compile())
            {
                submission.RunAndCompare();
            }
            else
            {
                for (int i = 0; i < submission.Output.TestCases.Count(); i++)
                {
                    submission.Output.TestCases[i].Pass = false;
                }
            }

            SubmissionService submissionService = new SubmissionService(dbContext);
            submissionService.AddSubmission(submission);
*/
            // Send the Submission to the compiler and get the output back
            // Object returned with contain a pass or fail bool, and compiler output
            

            // if it did not compile correctly, make the output the error message
            //      Print out compiler output
            //      Mark all test cases on the report as failed in submissionOutput
            // else
            //      Call the method to populate CodeInput, ExpectedOutput, Feedback -- Add in 10/30
            //      call the grading function passing the submission, ** 2d array of input output files/or assignment ID **, source code, language, runtime, and submission id
            //      get back the submission with the chekced fields updated
            
            // Update the database for the report ID to say which test cases were passed and save the output string for each
            // return submission / submission output to the user
        }

    }
}