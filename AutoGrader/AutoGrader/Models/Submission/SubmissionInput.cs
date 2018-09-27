using AutoGrader.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;

namespace AutoGrader.Models.Submission
{
    public class SubmissionInput
    {
        public SubmissionInput()
        {
        }

        public string SourceCode { get; set; }

        public Language Language { get; set; }
    }
}