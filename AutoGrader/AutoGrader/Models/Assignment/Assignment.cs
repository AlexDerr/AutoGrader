﻿using System;
using System.Collections.Generic;

namespace AutoGrader.Models.Assignment
{
    public class Assignment
    {
        public Assignment()
        {
            Languages = new List<string>();
            IO = new List<AssignmentIO>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public List<AssignmentIO> IO { get; set;}

        public int MemoryLimit { get; set; }

        public int TimeLimit { get; set; }

        public List<string> Languages { get; set; }
    }
}