using AutoGrader.DataAccess;
using AutoGrader.DataAccess.Services.ClassServices;
using AutoGrader.Models;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Users;
using AutoGrader.Models.ViewModels;
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

        public IActionResult StudentHome(User user)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            IEnumerable<Assignment> assignments = assignmentDataService.GetAssignments();

            //IEnumerable<Assignment> assignments = assignmentDataService.GetAssignmentsByUserId(Int32.Parse(user.Id));

            return View(assignments);
        }

        public IActionResult JoinClass()
        {
            //ClassDataService classDataService = new ClassDataService(dbContext);
            //IEnumerable<Class> classes = classDataService.GetClasses();

            //return View(classes);
            return PartialView("JoinClass", new JoinClassViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> JoinClass(string ClassKey)
        {
            ClassDataService classDataService = new ClassDataService(dbContext);


            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
