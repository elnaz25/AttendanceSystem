using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class Attendances
    {
        [Key]
        public int Attendance_ID { get; set; }

        public DateTime ComingTime { get; set; }

        [Display(Name ="Date")]
        public DateTime DateOfDay { get; set; }

        public DateTime? LeaveTime { get; set; }

        public int User_ID { get; set; }
    }
}