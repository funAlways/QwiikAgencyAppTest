using Microsoft.AspNetCore.Mvc;
using QwiikMVC.Models;
using System.Data.Entity;

namespace QwiikMVC.Controllers
{
    public class AppointmentController : Controller
    {
        AppDbContext db = new AppDbContext();

        public ActionResult Book()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Book(Appointment appt)
        {
            var setting = db.AgencySettings.First();
            DateTime targetDate = DateTime.Today;

            while (true)
            {
                int count = db.Appointments.Count(a =>
                    DbFunctions.TruncateTime(a.AppointmentDate) == targetDate);

                if (count < setting.MaxAppointmentsPerDay)
                    break;

                targetDate = targetDate.AddDays(1);
            }

            int lastToken = db.Appointments
                .Where(a => DbFunctions.TruncateTime(a.AppointmentDate) == targetDate)
                .OrderByDescending(a => a.TokenNumber)
                .Select(a => (int?)a.TokenNumber)
                .FirstOrDefault() ?? 0;

            appt.TokenNumber = lastToken + 1;
            appt.Status = "Pending";
            appt.AppointmentDate = targetDate;

            db.Appointments.Add(appt);
            db.SaveChanges();

            return RedirectToAction("Confirmation", new { id = appt.AppointmentId });
        }

        public ActionResult Confirmation(int id)
        {
            return View(db.Appointments.Find(id));
        }
    }

}
