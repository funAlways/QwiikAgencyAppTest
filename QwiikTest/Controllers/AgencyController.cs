using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

public class AgencyController : Controller
{
    AppDbContext db = new AppDbContext();

    public ActionResult DayView(DateTime? date)
    {
        DateTime target = date ?? DateTime.Today;

        var list = db.Appointments
            .Where(a => DbFunctions.TruncateTime(a.AppointmentDate) == target)
            .OrderBy(a => a.TokenNumber)
            .ToList();

        ViewBag.SelectedDate = target;
        return View(list);
    }

    public ActionResult Complete(int id)
    {
        var appt = db.Appointments.Find(id);
        appt.Status = "Completed";
        db.SaveChanges();
        return RedirectToAction("TodayQueue");
    }
}
