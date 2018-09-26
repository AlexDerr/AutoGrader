using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Assignment;

namespace AutoGrader.DataAccess
{
    public class AssignmentDataService : Service
    {
        public AssignmentDataService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<Assignment> GetAssignments()
        {
            return autoGraderDbContext.Assignments.ToList();
        }

        public Assignment GetSubmissionInputById(int id)
        {
            return GetAssignments().FirstOrDefault(e => e.Id == id);
        }
    }
}
