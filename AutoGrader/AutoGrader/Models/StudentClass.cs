using System;
using AutoGrader.Models.Users;

namespace AutoGrader.Models
{
    public class StudentClass
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public StudentClass() { }

        public StudentClass(Student s, Class c)
        {
            this.Student = s;
            this.StudentId = s.Id;
            this.Class = c;
            this.ClassId = c.Id;
        }
    }
}