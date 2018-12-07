using System.Collections.Generic;

namespace AutoGrader.Models.Submission
{
    public class SubmissionOutput
    {
        public SubmissionOutput()
        {
            Runtime = -1.0;
            TestCases = new List<TestCaseReport>();
        }

        public int Id { get; set; }

        public bool Compiled { get; set; }

        public double Runtime { get; set; }

        public string CompileOutput { get; set; }

        public int MemoryLimit { get; set; }

        public List<TestCaseReport> TestCases { get; set; }
    }
}