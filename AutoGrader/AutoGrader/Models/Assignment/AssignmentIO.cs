using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Assignment
{
    public class AssignmentIO
    {
        public AssignmentIO()
        {
            InputFileNames = new List<string>();
            OutputFileNames = new List<string>();
            Feedbacks = new List<string>();
        }

        public int ID { get; set; }

        public List<string> InputFileNames { get; set; }

        public List<string> OutputFileNames { get; set; }

        public List<string> Feedbacks { get; set; }
    }
}
