using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Assignment;

namespace AutoGrader.DataAccess
{
    public class TestCaseDataService : Service
    {
        public TestCaseDataService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<TestCaseSpecification> GetTestCasesSpecifications()
        {
            return autoGraderDbContext.TestCaseSpecifications.ToList();
        }

        public TestCaseSpecification GetTestCaseById(int id)
        {
            return GetTestCasesSpecifications().FirstOrDefault(e => e.ID == id);
        }

        public void AddTestCase(TestCaseSpecification testCase)
        {
            autoGraderDbContext.TestCaseSpecifications.Add(testCase);
        }

        public IEnumerable<TestCaseSpecification> GetTestCaseByAssignmentId(int id)
        {
            return autoGraderDbContext.TestCaseSpecifications.Where(e => e.AssignmentId == id);
        }
    }
}