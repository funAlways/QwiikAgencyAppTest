using Agency.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace QwiikMVC.Controllers
{
    public class AppointmentController : Controller
    {
        AppDbContext db = new AppDbContext();
        AppointmentService service = new AppointmentService(new AppointmentRepository());

        [HttpPost]
        public ActionResult Book(Appointment appt, DateTime RequestedDate)
        {
            var setting = db.AgencySettings.First();
            DateTime targetDate = RequestedDate;

            while (true)
            {
                int count = db.Appointments.Count(a => DbFunctions.TruncateTime(a.AppointmentDate) == targetDate);
                if (count < setting.MaxAppointmentsPerDay) break;
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


        public ActionResult Book()
        {
            return View();
        }
        public ActionResult Confirmation(int id)
        {
            return View(db.Appointments.Find(id));
        }
    }

}
