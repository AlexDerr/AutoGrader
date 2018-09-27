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

        public bool Compiled { get; set; }

        public int Runtime { get; set; }

        public string CompileOutput { get; set; }

        public List<TestCase> TestCases { get; set; }
    }
}