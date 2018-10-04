using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.Models.Assignment;

namespace AutoGrader.Methods.AssignmentMethod
{
    public class AssignmentMethod
    {
        public void UpdateAssignment(Assignment assignment)
        {
            // Figure out assignemnt id
            // Call service to update assignment to database
            // for list size of test cases 
            //       Check if it exists
            //       Call method to update/add test cases to the database
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