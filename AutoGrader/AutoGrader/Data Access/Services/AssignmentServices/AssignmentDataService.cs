﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;
using AutoGrader.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            return autoGraderDbContext.Assignments
                .Include(a => a.Submissions)
                .Include(a => a.TestCases)
                .FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Assignment> GetAssignmentsByClassId(int id)
        {
            return autoGraderDbContext.Assignments
                .Include(a => a.Submissions)
                .Where(e => e.ClassId == id);
        }

        public void AddAssignment(Assignment assignment)
        {
            autoGraderDbContext.Assignments.Add(assignment);
        }

        public Submission GetTopSubmissionForStudent(int assignmentId, int studentId)
        {
            return autoGraderDbContext.Assignments.FirstOrDefault(a => a.Id == assignmentId)
            .Submissions
            .Where(s => s.UserId == studentId)
            .OrderByDescending(s => s.Grade).FirstOrDefault();
        }


        public IEnumerable<Submission> GetStudentSubmissionsOnAssignment(int studentId, int assignmentId)
        {
            return autoGraderDbContext.Submissions.Where(s => s.UserId == studentId)
            .Where(a => a.AssignmentId == assignmentId)
            .Include(s => s.Input)
            .Include(s => s.Output)
            .ToList();
        }

        public IEnumerable<TestCaseSpecification> GetTestCases(int assignmentId)
        {
            return autoGraderDbContext.TestCaseSpecifications.ToList().Where(e => e.AssignmentId == assignmentId);
        }

        public void DeleteAssignment(Assignment assignment)
        {
            foreach (var sub in assignment.Submissions)
            {
                autoGraderDbContext.Submissions.Remove(sub);
                autoGraderDbContext.SubmissionInputs.Remove(sub.Input);
                autoGraderDbContext.SubmissionOutputs.Remove(sub.Output);
                autoGraderDbContext.TestCaseReports.RemoveRange(sub.Output.TestCases);
            }
            autoGraderDbContext.Assignments.Remove(assignment);
            autoGraderDbContext.TestCaseSpecifications.RemoveRange(assignment.TestCases);
        }

        public void UpdateAssignment(EditAssignmentViewModel model, Assignment a)
        {
            a.Name = model.Name;
            a.StartDate = model.StartDate;
            a.EndDate = model.EndDate;
            a.Description = model.Description;
            a.MemoryLimit = model.MemoryLimit;
            a.TimeLimit = model.TimeLimit;
            a.ClassId = model.ClassId;
            a.TestCases.RemoveAll(item => item.ID > 0);
            foreach(var item in model.TestCases)
            {
                item.AssignmentId = a.Id;
                a.TestCases.Add(item);
            }
            a.Languages = model.Languages;

            autoGraderDbContext.Assignments.Update(a);
        }

        public IEnumerable<Assignment> GetAssignmentsByInstructorId(int id)
        {
            IEnumerable<Class> classes = autoGraderDbContext.Classes.Where(f => f.InstructorId == id).Include(f => f.Assignments);
            List<Assignment> assignments = new List<Assignment>();

            foreach(var item in classes)
            {
                foreach(var ass in item.Assignments)
                {
                    assignments.Add(ass);
                }
            }

            return assignments;
        }
    }
}