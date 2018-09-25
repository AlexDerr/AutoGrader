using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.DataAccess;
using AutoGrader.Models.Assignment;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoGrader.Controllers
{
    public class AssignmentController : Controller
    {

        private AutoGraderDbContext dbContext;

        public AssignmentController(AutoGraderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(Assignment newAssignment)
        {
            if(!ModelState.IsValid) {
                return View("Index", newAssignment);
            }

            dbContext.Assignments.Add(newAssignment);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
