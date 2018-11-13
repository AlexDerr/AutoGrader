using AutoGrader.DataAccess;
using AutoGrader.DataAccess.Services.ClassServices;
using AutoGrader.Models;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
