using Microsoft.AspNetCore.Mvc;
using QwiikMVC.Models;
using System.Data.Entity;

public class AgencyController : Controller
{
    AppDbContext db = new AppDbContext();

    public ActionResult TodayQueue()
    {
        var today = DateTime.Today;
        var list = db.Appointments
            .Where(a => DbFunctions.TruncateTime(a.AppointmentDate) == today && a.Status == "Pending")
            .OrderBy(a => a.TokenNumber)
            .ToList();

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
