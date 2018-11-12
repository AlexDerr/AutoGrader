using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.ViewModels
{
    public class AssignmentViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required, DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        //public List<TestCase> IO { get; set; }

        [Required, DisplayName("Memory Limit")]
        public int MemoryLimit { get; set; }

        [Required, DisplayName("Time Limit")]
        public int TimeLimit { get; set; }

        [Required]
        public List<String> Languages { get; set; }

        [Required, DisplayName("Test Case 1 Input")]
        public string TestCase1Input { get; set; }

        [Required, DisplayName("Test Case 1 Output")]
        public string TestCase1Output { get; set; }

        [DisplayName("Test Case 2 Input")]
        public string TestCase2Input { get; set; }

        [DisplayName("Test Case 2 Output")]
        public string TestCase2Output { get; set; }
    }
}
