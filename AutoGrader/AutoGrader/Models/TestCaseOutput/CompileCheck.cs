using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.TestCaseOutput
{
    public class CompileCheck
    {
        public CompileCheck()
        {
        }

        public bool Compiled { get; set; }

        public string CompilerOutput { get; set; }
    }
}
