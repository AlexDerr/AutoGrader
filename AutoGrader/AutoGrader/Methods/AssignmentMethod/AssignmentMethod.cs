using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.Models.Assignment;

namespace AutoGrader.Methods.AssignmentMethod
{
    public class AssignmentMethod
    {
        public void AddAssignment(Assignment assignment)
        {
            // Call method to add assignment to database
            // for list size of IO 
            //       Call method to add IO to the database
        }

        public void UpdateAssignment(Assignment assignment)
        {
            // Figure out assignemnt id
            // Call method to update assignment to database
            // for list size of IO 
            //       Check if it exists
            //       Call method to update/add IO to the database
        }

        public List<Assignment> GetAssignments(int InstructorID)
        {
            List<Assignment> assignments = new List<Assignment>();
            // for all instructor assignments
            //      add each to a list of assignments
            return assignments;
        }
    }
}