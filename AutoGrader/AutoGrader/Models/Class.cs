using AutoGrader.Models.Users;
using System.Collections.Generic;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using AutoGrader.Methods.ClassMethod;
using AutoGrader.Models;

namespace AutoGrader.Models
{
    public class Class
    {
        public Class()
        {
            Assignments = new List<Assignment.Assignment>();
            StudentClasses = new List<StudentClass>();
        }

        public Class(ClassViewModel model)
        {
            Assignments = new List<Assignment.Assignment>();
            StudentClasses = new List<StudentClass>();

            this.Name = model.Name;
            this.ClassKey = ClassMethod.GenerateUniqueKey();
            this.InstructorId = model.InstructorId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassKey { get; set; }
        public int InstructorId { get; set; }
        public List<Assignment.Assignment> Assignments { get; set; }

        public IEnumerable<StudentClass> StudentClasses { get; set; }
    }
}