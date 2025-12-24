using Agency.DAL.Repositories;
using System;
using System.Linq;

public class AppointmentService
{
    IAppointmentRepository repo;
    AppDbContext db = new AppDbContext();

    public AppointmentService(IAppointmentRepository repo)
    {
        this.repo = repo;
    }

    public Appointment Book(Appointment appt)
    {
        int max = db.AgencySettings.First().MaxAppointmentsPerDay;
        DateTime targetDate = DateTime.Today;

        while (repo.GetDailyCount(targetDate) >= max)
            targetDate = targetDate.AddDays(1);

        appt.TokenNumber = repo.GetLastToken(targetDate) + 1;
        appt.AppointmentDate = targetDate;
        appt.Status = "Pending";

        repo.Add(appt);
        return appt;
    }
}
