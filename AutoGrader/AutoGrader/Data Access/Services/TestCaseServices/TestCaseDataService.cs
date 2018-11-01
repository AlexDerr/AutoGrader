using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Assignment;

namespace AutoGrader.DataAccess
{
    public class TestCaseDataService : Service
    {
        public TestCaseDataService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<TestCase> GetTestCases()
        {
            return autoGraderDbContext.TestCases.ToList();
        }

        public TestCase GetTestCaseById(int id)
        {
            return GetTestCases().FirstOrDefault(e => e.ID == id);
        }

        public void AddTestCase(TestCase testCase)
        {
            autoGraderDbContext.TestCases.Add(testCase);
        }

        public IEnumerable<TestCase> GetTestCaseByAssignmentId(int id)
        {
            return autoGraderDbContext.TestCases.Where(e => e.AssignmentId == id);
        }
    }
}