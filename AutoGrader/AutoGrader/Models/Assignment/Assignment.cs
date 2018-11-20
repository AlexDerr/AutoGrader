using AutoGrader.Models.Submission;
using AutoGrader.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGrader.Models.Assignment
{
    public class Assignment
    {
        public Assignment()
        {
            TestCases = new List<TestCaseSpecification>();
            Languages = new List<String>();
            Submissions = new List<SubmissionInput>();
        }

        public Assignment(AssignmentViewModel model)
        {
            Languages = new List<String>();
            TestCases = new List<TestCaseSpecification>();
            Submissions = new List<SubmissionInput>();

            this.Name = model.Name;
            this.StartDate = model.StartDate;
            this.EndDate = model.EndDate;
            this.Description = model.Description;
            this.MemoryLimit = model.MemoryLimit;
            this.TimeLimit = model.TimeLimit;
            this.Languages = model.Languages;
            this.ClassId = model.ClassId;
            this.TestCases.Add(new TestCaseSpecification(model.TestCase1Input, model.TestCase1Output));
            this.TestCases.Add(new TestCaseSpecification(model.TestCase2Input, model.TestCase2Output));
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int ClassId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public List<TestCaseSpecification> TestCases { get; set; }

        public int MemoryLimit { get; set; }

        public int TimeLimit { get; set; }

        public List<String> Languages { get; set; }

        public List<SubmissionInput> Submissions { get; set; }
    }
}