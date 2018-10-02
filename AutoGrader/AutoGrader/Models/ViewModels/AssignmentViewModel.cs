using AutoGrader.Models.Assignment;
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

        public List<AssignmentIO> IO { get; set; }

        [Required, DisplayName("Memory Limit")]
        public int MemoryLimit { get; set; }

        [Required, DisplayName("Time Limit")]
        public int TimeLimit { get; set; }

        [Required]
        public List<string> Languages { get; set; }

        [Required, DisplayName("Test Case 1")]
        public string TestCase1 { get; set; }

        [DisplayName("Test Case 2")]
        public string TestCase2 { get; set; }

    }
}
