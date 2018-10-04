using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Submission
{
    public class TestCaseReport
    {
        public TestCaseReport()
        {
        }

        public bool PassFail { get; set; }

        public string CodeInput { get; set; }

        public string CodeOutput { get; set; }

        public string ExpectedOutput { get; set; }

        public string Feedback { get; set; }

        public int Runtime { get; set; }
    }
}