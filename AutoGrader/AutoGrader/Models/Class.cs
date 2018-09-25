using AutoGrader.Models.Users;
using System.Collections.Generic;
using AutoGrader.Models.Assignment;

namespace AutoGrader.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Instructor Instructor { get; set; }
        public List<Assignment.Assignment> Assignments { get; set; }
    }
}