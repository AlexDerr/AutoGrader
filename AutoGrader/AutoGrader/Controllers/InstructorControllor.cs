using AutoGrader.DataAccess;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Controllers
{
    public class InstructorControllor : Controller
    {
        private AutoGraderDbContext dbContext;

        public InstructorControllor(AutoGraderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult InstructorHome(User user)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            IEnumerable<Assignment> assignments = assignmentDataService.GetAssignments();

            return View(assignments);
        }

        public IActionResult CreateClass()
        {
            return View();
        }

        //POst for create class
    }
}
