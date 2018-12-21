using AutoGrader.DataAccess;
using AutoGrader.DataAccess.Services;
using AutoGrader.DataAccess.Services.ClassServices;
using AutoGrader.Models;
using AutoGrader.Models.Assignment;
using AutoGrader.Models.Users;
using AutoGrader.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
            StudentClassDataService studentClassDataService = new StudentClassDataService(dbContext);
            var classes = studentClassDataService.GetClassesByStudentId(student.Id);
            return View(classes);
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


            if (c != null)
            {
                StudentClassDataService studentClassDataService = new StudentClassDataService(dbContext);
                bool inClass = studentClassDataService.InClass(student, c);

                if (!inClass)
                {
                    studentClassDataService.AddStudentClass(student, c);
                    await dbContext.SaveChangesAsync();
                }
            }

            return RedirectToAction("StudentHome", "Student");
        }

        public async Task<IActionResult> LeaveClass(bool confirm, int classId)
        {
            StudentClassDataService studentClassDataService = new StudentClassDataService(dbContext);

            StudentDataService studentDataService = new StudentDataService(dbContext);
            ClassDataService classDataService = new ClassDataService(dbContext);

            student = studentDataService.GetStudentByUsername(UserManager.GetUserName(User));
            Class c = classDataService.GetClassById(classId);

            studentClassDataService.RemoveStudentClass(student, c);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("StudentHome", "Student");
        }
    }
}
