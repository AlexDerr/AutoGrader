﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;
using AutoGrader.Models.ViewModels;

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

        //public void UpdateAssignment(Assignment a, AssignmentViewModel model)
        //{
        //    a.Name = model.Name;
        //    a.StartDate = model.StartDate;
        //    a.EndDate = model.EndDate;
        //    a.Description = model.Description;
        //    a.MemoryLimit = model.MemoryLimit;
        //    a.TimeLimit = model.TimeLimit;
        //    a.ClassId = model.ClassId;
        //    a.TestCases = model.TestCases;
        //    autoGraderDbContext.Assignments.Update(a);
        //}
    }
}