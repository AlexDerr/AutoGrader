using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.DataAccess;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Submission;
using AutoGrader.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ShellHelper;
using AutoGrader.Methods.GraderMethod;
using AutoGrader.Models;
using AutoGrader.Models.Users;
using AutoGrader.DataAccess.Services.ClassServices;

namespace AutoGrader.Controllers
{
    public class InstructorController : Controller
    {
        private AutoGraderDbContext dbContext;

        public InstructorController(AutoGraderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult InstructorHome(User user)
        {
            ClassDataService classDataService = new ClassDataService(dbContext);
            IEnumerable<Class> classes = classDataService.GetClasses();

            return View(classes);
        }

        public IActionResult CreateClass()
        {
            return View();
        }

        //POst for create class
        [HttpPost]
        public async Task<IActionResult> CreateClass(ClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                Class thisClass = new Class(model);
                ClassDataService classDataService = new ClassDataService(dbContext);
                classDataService.AddClass(thisClass);
            }

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
