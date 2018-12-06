using AutoGrader.Data_Access.Services.UserDataService;
using AutoGrader.DataAccess;
using AutoGrader.DataAccess.Services;
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
            //student = studentDataService.GetStudentByUsername(user.UserName);

            if(c != null)
            {
                StudentClassDataService studentClassDataService = new StudentClassDataService(dbContext);

                studentClassDataService.AddStudentClass(student, c);

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("StudentHome", "Student");
        }

        public IActionResult ViewAssignments(int classId)
        {
            AssignmentDataService assignmentDataService = new AssignmentDataService(dbContext);
            IEnumerable<Assignment> assignments = assignmentDataService.GetAssignmentsByClassId(classId);

            StudentDataService studentDataService = new StudentDataService(dbContext);
            student = studentDataService.GetStudentByUsername(UserManager.GetUserName(User));

            ViewData.Add("StudentId", student.Id);

            return View(assignments);
        }
    }
}
