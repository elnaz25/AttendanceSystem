using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AttendanceSystem.Controllers
{
    public class AdminController : Controller
    {
        private AttendanceDb _Db = new AttendanceDb();

        public ActionResult Login(string _Urls)
        {
            string AdminEmail = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            string Password = ConfigurationManager.AppSettings["Password"].ToString();

            var user = _Db.Users.Where(c => c.Email == AdminEmail);
            if(user.Count() == 0)
            {
                Users users = new Users
                {
                    FirstName = "admin",
                    LastName = "admini",
                    BirthDate = Convert.ToDateTime("25/11/1376"),
                    Salary = 0,
                    Email = AdminEmail,
                    Password = Password,
                    ConfirmPassword = Password,
                    UserRole = "Admin"
                };

                _Db.Users.Add(users);
                _Db.SaveChanges();
            }
            return View();
        }

        public ActionResult Login(LoginViewModel viewModel, string _Urls)
        {
            if (ModelState.IsValid)
            {
                string Email = viewModel.Email;
                string Password = viewModel.Password;

                bool userValid = _Db.Users.Any(u => u.Email == Email && u.Password == Password);
                IEnumerable user = _Db.Users.Where(obj => obj.Email == Email);
                var result = _Db.Users.SingleOrDefault(c => c.Email == Email);
                var tiket = JsonConvert.SerializeObject(result);

                if(userValid)
                {
                    FormsAuthentication.SetAuthCookie(tiket, false);
                    
                    if (Url.IsLocalUrl(_Urls) && _Urls.Length > 1 && _Urls.StartsWith("/") && !_Urls.StartsWith("//") && !_Urls.StartsWith("/\\"))
                    {
                        return Redirect(_Urls);
                    }
                    else
                    {
                        return RedirectToAction("Index" ,"Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The username or password provided is incorrect");
                }
            }

            return View(viewModel);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register(int id)
        {
            return View();
        }
    }
}