using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;

namespace AutoGrader.DataAccess
{
    public class SubmissionService : Service
    {
        public SubmissionService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<Submission> GetSubmissions()
        {
            return autoGraderDbContext.Submissions.ToList();
        }

        public Submission GetSubmissionById(int id)
        {
            return GetSubmissions().FirstOrDefault(e => e.SubmissionId == id);
        }

        public void AddSubmission(Submission submission)
        {
            autoGraderDbContext.Submissions.Add(submission);
        }
    }
}