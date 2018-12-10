using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Submission;
using Microsoft.EntityFrameworkCore;

namespace AutoGrader.DataAccess
{
    public class SubmissionDataService : Service
    {
        public SubmissionDataService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<Submission> GetSubmissions()
        {
            return autoGraderDbContext.Submissions.ToList();
        }

        public Submission GetSubmissionById(int id)
        {
            return autoGraderDbContext.Submissions
                .Include(s => s.Input)
                .Include(s => s.Output)
                    .ThenInclude(o => o.TestCases)
                .FirstOrDefault(e => e.SubmissionId == id);


            //return GetSubmissions()
            //.FirstOrDefault(e => e.SubmissionId == id);
        }

        public void AddSubmission(Submission submission)
        {
            autoGraderDbContext.Submissions.Add(submission);
        }
    }
}