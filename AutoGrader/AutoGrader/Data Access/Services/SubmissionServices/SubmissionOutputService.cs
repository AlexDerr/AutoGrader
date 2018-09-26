using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Submission;

namespace AutoGrader.DataAccess.Services.SubmissionServices
{
    public class SubmissionOutputService : Service
    {
        public SubmissionOutputService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<SubmissionOutput> GetSubmissionOutputs()
        {
            return autoGraderDbContext.SubmissionOutputs.ToList();
        }

        public SubmissionOutput GetSubmissionOutputById(int id)
        {
            return GetSubmissionOutputs().FirstOrDefault(e => e.SubmissionOutputId == id);
        }
    }
}
