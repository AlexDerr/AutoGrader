using AutoGrader.Models.TestCaseReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Submission
{
    public class SubmissionOutput
    {
        public SubmissionOutput()
        {
            TestCases = new List<TestCase>();
        }

        public int UserId { get; set; }

        public int SubmissionInputId { get; set; }

        public int SubmissionOutputId { get; set; }

        public int AssignmentId { get; set; }

        public DateTime SubmissionTime { get; set; }

        public string SourceCodeFileName { get; set; }

        public List<TestCase> TestCases { get; set; }

        public string Language { get; set; }
    }
}