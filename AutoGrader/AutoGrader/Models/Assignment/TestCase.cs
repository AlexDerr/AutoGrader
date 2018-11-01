using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Assignment
{
    public class TestCase
    {
        public TestCase()
        {
        }

        public TestCase(string Input, string Output)
        {
            this.Input = Input;
            this.ExpectedOutput = Output;
        }

        public int ID { get; set; }

        public string Input { get; set; }

        public string ExpectedOutput { get; set; }

        public string Feedback { get; set; }

        public int AssignmentId { get; set; }
    }
}
