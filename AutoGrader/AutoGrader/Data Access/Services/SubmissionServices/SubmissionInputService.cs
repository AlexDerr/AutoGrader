using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models.Submission;

namespace AutoGrader.DataAccess.Services.SubmissionServices
{
    public class SubmissionInputService : Service
    {
        public SubmissionInputService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<SubmissionInput> GetSubmissionInputs(){
            return autoGraderDbContext.SubmissionInputs.ToList();
        }

        public SubmissionInput GetSubmissionInputById(int id) {
            return GetSubmissionInputs().FirstOrDefault(e => e.SubmissionInputId == id);
        }
    }
}
