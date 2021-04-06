using AttendanceSystem.Data;
using AttendanceSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace AttendanceSystem.Controllers
{
    public class UsersController : Controller
    {
        private AttendanceDb _Db = new AttendanceDb();

        [System.Web.Http.Authorize]
        [System.Web.Http.Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_Db.Users.ToList());
        }

        public ActionResult Datails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users users = _Db.Users.Find(id);

            if(users == null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName, Salary, BirthDate, Email, Password, ConfirmPassword, UserRole")] Users users)
        {
            if(ModelState.IsValid)
            {
                _Db.Users.Add(users);
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        public ActionResult Edite(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users users = _Db.Users.Find(id);
            if( users == null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Edite")]
        [ValidateAntiForgeryToken]
        public ActionResult Edite([Bind(Include = "ID,FirstName,LastName, Salary, BirthDate, Email, Password, ConfirmPassword, UserRole")] Users users)
        {
            if (ModelState.IsValid)
            {
                _Db.Entry(users).State = EntityState.Modified;
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users users = _Db.Users.Find(id);
            if(users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Users users = _Db.Users.Find(id);
            _Db.Users.Remove(users);
            _Db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}