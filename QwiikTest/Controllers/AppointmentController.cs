using Agency.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace QwiikMVC.Controllers
{
    public class AppointmentController : Controller
    {
        AppDbContext db = new AppDbContext();
        AppointmentService service = new AppointmentService(new AppointmentRepository());

        [HttpPost]
        public ActionResult Book(Appointment appt)
        {
            var result = service.Book(appt);
            return RedirectToAction("Confirmation", new { id = result.AppointmentId });
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
