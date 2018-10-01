using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.TestCaseReport
{
    public class TestCase
    {
        public TestCase()
        {
        }

        public bool PassFail { get; set; }

         public string CodeOutput { get; set; }

        public string Feedback { get; set; }

        public int Runtime { get; set; }
    }
}