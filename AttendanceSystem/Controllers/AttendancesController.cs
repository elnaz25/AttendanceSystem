using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace AttendanceSystem.Controllers
{
    public class AttendancesController : Controller
    {
        private AttendanceDb _Db = new AttendanceDb();

        public ActionResult Index()
        {
            Users userInfo = JsonConvert.DeserializeObject<Users>(User.Identity.Name);
            int userId = Int32.Parse(userInfo.ID.ToString());

            List<Attendances> attendances = _Db.Attendances.ToList().Where(c => c.User_ID == userId).ToList();

            return View(attendances);
        }

        public ActionResult Total (int? people)
        {
            List<Users> users = _Db.Users.ToList();
            List<Attendances> attendances = _Db.Attendances.ToList();

            List<SelectListItem> selectLists = new List<SelectListItem>();

            foreach(var i in users)
            {
                selectLists.Add(new SelectListItem
                {
                    Value = i.ID.ToString(),
                    Text = i.FirstName + i.LastName
                });
            }

            TempData["users"] = selectLists;
            TempData["usersName"] = users;

            return View(attendances);
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("TotalAttendance")]
        [ValidateAntiForgeryToken]
        public ActionResult Total(int? Users , int ? people , string start , string end)
        {
            List<Attendances> attendancesList;

            if (Users != null)
            {
                if (start != "" && end != "")
                {
                    DateTime dateStart = Convert.ToDateTime(start);
                    DateTime dataEnd = Convert.ToDateTime(end);

                    attendancesList = _Db.Attendances.ToList().Where(c => c.User_ID == Users && c.DateOfDay >= dateStart && dataEnd >= c.DateOfDay).ToList();
                }

                else
                {
                    attendancesList = _Db.Attendances.ToList().Where(c => c.User_ID == Users).ToList();
                }
            }
            else if (start != "" && end != "" && Users == null)
            {
                DateTime dateStart = Convert.ToDateTime(start);
                DateTime dataEnd = Convert.ToDateTime(end);

                attendancesList = _Db.Attendances.ToList().Where(c => c.User_ID == Users && c.DateOfDay >= dateStart && dataEnd >= c.DateOfDay).ToList();
            }
            else
            {
                attendancesList = _Db.Attendances.ToList();
            }

            List<Users> users = _Db.Users.ToList();
            List<SelectListItem> selectLists = new List<SelectListItem>();

            foreach (var i in users)
            {
                selectLists.Add(new SelectListItem
                {
                    Value = i.ID.ToString(),
                    Text = i.FirstName + i.LastName
                });
            }

            TempData["users"] = selectLists;
            TempData["usersName"] = users;
            TempData["Start"] = start;
            TempData["End"] = end;

            return View(attendancesList);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendances attendances = _Db.Attendances.Find(id);
            if (attendances == null)
            {
                return HttpNotFound();
            }
            return View(attendances);
        }


        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Edite")]
        [ValidateAntiForgeryToken]
        public ActionResult Edite(Attendances attendances)
        {
            if (ModelState.IsValid)
            {
                _Db.Entry(attendances).State = EntityState.Modified;
                _Db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(attendances);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Attendances attendances = _Db.Attendances.Find(id);
            if(attendances == null)
            {
                return HttpNotFound();
            }

            return View(attendances);
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Attendances attendances = _Db.Attendances.Find(id);
            _Db.Attendances.Remove(attendances);
            _Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}