using AutoGrader.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models
{
    public class StudentClass
    {
        public Student student { get; set; }
        public Class c { get; set; }
        public int studentId { get; set; }
        public int classId { get; set; }
    }
}