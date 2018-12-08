using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;

namespace AutoGrader.DataAccess
{
    public class AssignmentDataService : Service
    {
        public AssignmentDataService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<Assignment> GetAssignments()
        {
            return autoGraderDbContext.Assignments.ToList();
        }

        public Assignment GetAssignmentById(int id)
        {
            return GetAssignments().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Assignment> GetAssignmentsByClassId(int id)
        {
            return GetAssignments().Where(e => e.ClassId == id);
        }

        public void AddAssignment(Assignment assignment)
        {
            autoGraderDbContext.Assignments.Add(assignment);
        }

        public Submission GetTopSubmissionForStudent(int assignmentId, int studentId)
        {
            return autoGraderDbContext.Assignments.FirstOrDefault(a => a.Id == assignmentId)
            .Submissions.OrderByDescending(s => s.Grade).FirstOrDefault();
        }


        public List<Submission> GetStudentSubmissionsOnAssignment(int studentId, int assignmentId)
        {
            return autoGraderDbContext.Submissions.Where(s => s.UserId == studentId)
                .Where(a => a.AssignmentId == assignmentId)
                .ToList();
        }

        public IEnumerable<TestCaseSpecification> GetTestCases(int assignmentId)
        {
            return autoGraderDbContext.TestCaseSpecifications.ToList().Where(e => e.AssignmentId == assignmentId);
        }

        //public IEnumerable<Assignment> GetAssignmentsByUserId(int UserId)
        //{
        //    yield return GetAssignments().FirstOrDefault(e => e.ClassId.Students.FirstOrDefault(f => f.Id == UserId).Id == UserId);
        //}
    }
}