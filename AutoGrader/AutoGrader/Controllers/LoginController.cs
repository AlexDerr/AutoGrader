using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGrader.DataAccess;
using AutoGrader.Models;
using AutoGrader.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoGrader.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Todo - Logic connecting to the backend for successful login
            }
            else
            {

                ModelState.AddModelError("", "Invalid login credentials.");
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(model);
                }

                if (model.IsInstructor == true)
                {
                    //Todo - register instructor into the DB and route them to their home page
                }
                else if (model.IsStudent == true)
                {
                    //Todo - register student into the DB and route them to their home page
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid registration");
                return View(model);
            }

            return View(model);

        }
    }
}