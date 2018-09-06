using System;

namespace AutoGrader.Models.Submission
{
    public class Submission
    {
        public Submission()
        {
        }

        public int UserId { get; set; }

        public int SubmissionId { get; set; }

        public int AssignmentId { get; set; }

        public DateTime SubmissionTime { get; set; }

        public string SourceCodeFileName { get; set; }

        public string Language { get; set; }
    }
}
