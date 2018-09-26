using AutoGrader.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;

namespace AutoGrader.Models.Submission
{
    public class SubmissionInput
    {
        public SubmissionInput(SubmissionInputViewModel input)
        {
            this.SourceCode = input.SourceCode;
        }

        public int SubmissionInputId { get; set; }

        public int UserId { get; set; }

        public int AssignmentId { get; set; }

        public DateTime SubmissionTime { get; set; }

        public string SourceCodeFileName { get; set; }

        public string Language { get; set; }

        public string SourceCode { get; set; }
    }
}