using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Submission
{
    public class CompileCheckOutput
    {
        public CompileCheckOutput()
        {
        }

        public bool Compiled { get; set; }

        public string CompilerOutput { get; set; }
    }
}