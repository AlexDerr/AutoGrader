using System;
using System.Collections.Generic;

namespace AutoGrader.DataAccess
{
    public class Service
    {
        protected readonly AutoGraderDbContext autoGraderDbContext;

        public Service(AutoGraderDbContext autoGraderDbContext)
        {
            this.autoGraderDbContext = autoGraderDbContext;
        }
    }
}
