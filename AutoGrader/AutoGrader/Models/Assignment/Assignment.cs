using System;
using System.Collections.Generic;

namespace AutoGrader.Models.Assignment
{ 
    public class Assignment
    {
	    public Assignment()
	    {
            InputFileNames = new List<string>();
            OutputFileNames = new List<string>();
            Feedbacks = new List<string>();
            Languages = new List<string>();
	    }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public List<string> InputFileNames { get; set; }

        public List<string> OutputFileNames { get; set; }

        public List<string> Feedbacks { get; set; }

        public int MemoryLimit { get; set; }

        public int TimeLimit { get; set; }

        public List<string> Languages { get; set; }
    }
}