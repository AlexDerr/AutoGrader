using AutoGrader.DataAccess;
using AutoGrader.Models.Assignment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Controllers
{
    public class StudentController : Controller
    {
        private AutoGraderDbContext dbContext;

        public StudentController(AutoGraderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult StudentHome()
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            IEnumerable<Assignment> assignments = assignmentDataService.GetAssignments();

            return View(assignments);
        }
    }
}
