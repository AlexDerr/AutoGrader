using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.DataAccess;
using AutoGrader.Models.Assignment;

namespace AutoGrader.Methods.AssignmentMethod
{
    public class AssignmentMethod
    {
        public void UpdateAssignment(Assignment assignment, AutoGraderDbContext dbContext)
        {
            // Figure out assignemnt id
            // Call service to update assignment to database
            // for list size of test cases 
            //       Check if it exists
            //       Call method to update/add test cases to the database
        }

        public List<Assignment> GetAssignmentsForInstructor(int InstructorID, AutoGraderDbContext dbContext)
        {
            return null;
//            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
//            IEnumerable<Assignment> assignments = assignmentDataService.GetAssignmentById(InstructorID);
            // for all instructor assignments
            //      add each to a list of assignments
//            return assignments.ToList();
        }

        public List<Assignment> GetAssignmentsForStudent(int StudentID, AutoGraderDbContext dbContext)
        {
            return null;
            //            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            //            IEnumerable<Assignment> assignments = assignmentDataService.GetAssignmentById(StudentID);
            // for all instructor assignments
            //      add each to a list of assignments
            //            return assignments.ToList();
        }
    }
}