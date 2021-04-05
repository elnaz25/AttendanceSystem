using AttendanceSystem.Data;
using AttendanceSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AttendanceDb _Db = new AttendanceDb();
        private readonly DateTime myDateTime;

        [Authorize]
        public ActionResult Index(AttendanceViewModel viewmodels)
        {
            Users userinfo = JsonConvert.DeserializeObject<Users>(User.Identity.Name);
            DateTime today_Date = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            bool AttendanceValid = _Db.Attendances.Any(b => b.DateOfDay == today_Date && b.User_ID == userinfo.ID);

            if (AttendanceValid == true)
            {
                viewmodels.IsComing = true;
                bool Attendance_TotalValid = _Db.Attendances.Any(b => b.DateOfDay == today_Date && b.User_ID == userinfo.ID && b.LeaveTime != null);

                if (Attendance_TotalValid)
                {
                    viewmodels.IsLeave = true;
                }
            }

            else
            {
                viewmodels.IsComing = false;
            }

            return View(viewmodels);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(Attendances attendances, AttendanceViewModel viewModels)
        {
            Users userinfo = JsonConvert.DeserializeObject<Users>(User.Identity.Name);
            DateTime dateTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime today_Date = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

            bool AttendanceValid = _Db.Attendances.Any(b => b.DateOfDay == today_Date && b.User_ID == userinfo.ID);

            if (AttendanceValid)
            {
                var attendance = _Db.Attendances.Where(b => b.DateOfDay == today_Date && b.User_ID == userinfo.ID).Single();
                viewModels.IsComing = true;
                viewModels.IsLeave = true;
                attendances.LeaveTime = myDateTime;

                _Db.Entry(attendance).State = EntityState.Modified;
                _Db.SaveChanges();

                return View(viewModels);
            }
            else
            {
                int userID = userinfo.ID;
                DateTime date_now = DateTime.Now;
                viewModels.IsLeave = false;

                Attendances Attendances = new Attendances
                {
                    User_ID = userID,
                    ComingTime = myDateTime,
                    DateOfDay = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")),
                };

                _Db.Attendances.Add(Attendances);
                _Db.SaveChanges();
                viewModels.IsComing = true;
                return View(viewModels);
            }
        }
    }
}