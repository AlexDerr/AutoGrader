using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Submission
{
    public class SubmissionReport
    {
        public SubmissionReport()
        {
            Comments = new List<string>();
        }

        public int UserId { get; set; }

        public int SubmissionId { get; set; }

        public int ReportId { get; set; }

        public int AssignmentId { get; set; }

        public DateTime SubmissionTime { get; set; }

        public int ExecutionTime { get; set; }

        public string SourceCodeFileName { get; set; }

        public bool[] TestCases { get; set; }

        public List<string> Comments { get; set; }

        public string Language { get; set; }
    }
}
