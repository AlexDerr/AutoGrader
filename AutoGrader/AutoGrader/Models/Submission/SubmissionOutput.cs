﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Submission
{
    public class SubmissionOutput
    {
        public SubmissionOutput()
        {
            Runtime = -1.0;
            TestCases = new List<TestCaseReport>();
        }

        public bool Compiled { get; set; }

        public double Runtime { get; set; }

        public string CompileOutput { get; set; }

        public List<TestCaseReport> TestCases { get; set; }
    }
}