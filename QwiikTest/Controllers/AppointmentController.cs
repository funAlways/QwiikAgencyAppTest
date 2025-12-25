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
        public ActionResult Book(Appointment appt, DateTime requestedDate)
        {
            service.Book(appt, requestedDate);

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
