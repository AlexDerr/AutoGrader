using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AutoGrader.Models;
using Microsoft.AspNetCore.Http;
using AutoGrader.Models.Submission;
using AutoGrader.Models.ViewModels;
using System;
using ShellHelper;
using Microsoft.AspNetCore.Identity;
using AutoGrader.Data_Access.Services.UserDataService;
using AutoGrader.DataAccess;
using AutoGrader.Models.Users;

namespace AutoGrader.Controllers
{
    public class HomeController : Controller
    {
        private AutoGraderDbContext dbContext;
        private SignInManager<IdentityUser> SignInManager;
        private UserManager<IdentityUser> UserManager;

        public HomeController(AutoGraderDbContext autoGraderDbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.dbContext = autoGraderDbContext;
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (SignInManager.IsSignedIn(User))
            {
                UserDataService userDataService = new UserDataService(dbContext);
                User user = userDataService.GetUserByUsername(UserManager.GetUserName(User));

                if (user.Role == "Student")
                    return RedirectToAction("StudentHome", "Student", user);
                else
                    return RedirectToAction("InstructorHome", "Instructor", user);
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
