using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.TestCaseReport
{
    public class TestCaseReport
    {
        public TestCaseReport()
        {
        }

        public bool PassFail { get; set; }

        public string CompilerOutput { get; set; }

        public int Runtime { get; set; }
    }
}
