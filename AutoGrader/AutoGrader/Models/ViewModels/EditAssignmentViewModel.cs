using AutoGrader.Models.Assignment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.ViewModels
{
    public class EditAssignmentViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required, DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        public List<TestCaseSpecification> IO { get; set; }

        [Required, DisplayName("Memory Limit (kB)")]
        public int MemoryLimit { get; set; }

        [Required, DisplayName("Time Limit (ms)")]
        public int TimeLimit { get; set; }

        [Required]
        public List<String> Languages { get; set; }

        //[Required, DisplayName("Test Case 1 Input")]
        //public string TestCase1Input { get; set; }

        //[Required, DisplayName("Test Case 1 Output")]
        //public string TestCase1Output { get; set; }

        //[DisplayName("Test Case 2 Input")]
        //public string TestCase2Input { get; set; }

        //[DisplayName("Test Case 2 Output")]
        //public string TestCase2Output { get; set; }

        [DisplayName("Number of Test Case")]
        public int NumberOfTestCases { get; set; }

        [DisplayName("Test Case")]
        public List<TestCaseSpecification> TestCases { get; set; }

        public int ClassId { get; set; }
    }
}
