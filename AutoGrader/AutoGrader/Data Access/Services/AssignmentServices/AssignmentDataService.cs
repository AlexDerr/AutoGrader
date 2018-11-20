﻿using System;
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

        public Assignment GetAssignmentById(int id)
        {
            return GetAssignments().FirstOrDefault(e => e.Id == id);
        }

        public void AddAssignment(Assignment assignment)
        {
            autoGraderDbContext.Assignments.Add(assignment);
        }

        //public IEnumerable<Assignment> GetAssignmentsByUserId(int UserId)
        //{
        //    yield return GetAssignments().FirstOrDefault(e => e.ClassId.Students.FirstOrDefault(f => f.Id == UserId).Id == UserId);
        //}
    }
}