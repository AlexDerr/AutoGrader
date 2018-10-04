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

        public int ID { get; set; }

        public string Input { get; set; }

        public string ExpectedOutput { get; set; }

        public string Feedback { get; set; }
    }
}
