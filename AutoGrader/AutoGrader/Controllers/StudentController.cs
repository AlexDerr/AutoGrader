using AutoGrader.Data_Access.Services.UserDataService;
using AutoGrader.DataAccess;
using AutoGrader.DataAccess.Services.ClassServices;
using AutoGrader.Models;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Users;
using AutoGrader.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
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
        private SignInManager<IdentityUser> SignInManager;
        private UserManager<IdentityUser> UserManager;
        private Student student;

        public StudentController(AutoGraderDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.dbContext = dbContext;
            this.SignInManager = signInManager;
            this.UserManager = userManager;
        }

        public IActionResult StudentHome(User user)
        {
            StudentDataService studentDataService = new StudentDataService(dbContext);
            student = studentDataService.GetStudentByUsername(UserManager.GetUserName(User));
            ViewData["Id"] = student.Id;
            return View(student.Classes);
        }

        public IActionResult JoinClass()
        {
            return PartialView("JoinClass", new JoinClassViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> JoinClass(string ClassKey)
        {
            ClassDataService classDataService = new ClassDataService(dbContext);
            StudentDataService studentDataService = new StudentDataService(dbContext);
            student = studentDataService.GetStudentByUsername(UserManager.GetUserName(User));

            Class c = classDataService.GetClassByKey(ClassKey);
            //student = studentDataService.GetStudentByUsername(user.UserName);

            classDataService.AddStudent(student, c);

            studentDataService.AddClass(student, c);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("StudentHome", "Student");
        }
    }
}
