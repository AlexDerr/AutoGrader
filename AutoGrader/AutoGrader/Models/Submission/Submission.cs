using AutoGrader.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.Submission
{
    public class Submission
    {
        public Submission() 
        {
            Input = new SubmissionInput();
            Output = new SubmissionOutput();
        }

        public int SubmissionId { get; set; }

        public int UserId { get; set; }

        public int AssignmentId { get; set; }

        public DateTime SubmissionTime { get; set; }

        public SubmissionInput Input { get; set; }

        public SubmissionOutput Output { get; set; }
    }
}
