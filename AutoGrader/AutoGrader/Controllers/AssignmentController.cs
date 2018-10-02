using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.DataAccess;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.ViewModels;
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

        [HttpPost]
        public async Task<IActionResult> Create(AssignmentViewModel model)
        {
            if(!ModelState.IsValid) {
                return View("Index", model);
            }

            //dbContext.Assignments.Add(model);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
