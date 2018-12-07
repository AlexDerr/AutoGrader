using System;

namespace AutoGrader.Models.Submission
{
    public class Submission
    {
        public Submission() 
        {
            Input = new SubmissionInput();
            Output = new SubmissionOutput();
            Grade = 0;
        }

        public int SubmissionId { get; set; }

        public int UserId { get; set; }

        public int AssignmentId { get; set; }

        public double Grade { get; set; }

        public DateTime SubmissionTime { get; set; }

        public SubmissionInput Input { get; set; }

        public SubmissionOutput Output { get; set; }
    }
}
