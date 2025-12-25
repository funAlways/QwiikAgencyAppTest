using Agency.DAL.Repositories;
using System;
using System.Data.Entity;
using System.Linq;

public class AppointmentService
{
    IAppointmentRepository repo;
    AppDbContext db = new AppDbContext();

    public AppointmentService(IAppointmentRepository repo)
    {
        this.repo = repo;
    }
    public Appointment Book(Appointment appt, DateTime requestedDate)
    {
        int max = db.AgencySettings.First().MaxAppointmentsPerDay;
        DateTime targetDate = requestedDate;

        while (true)
        {
            bool isHoliday = db.PublicHolidays.Any(h =>
                DbFunctions.TruncateTime(h.HolidayDate) == targetDate);

            if (isHoliday)
            {
                targetDate = targetDate.AddDays(1);
                continue;
            }

            if (repo.GetDailyCount(targetDate) < max)
                break;

            targetDate = targetDate.AddDays(1);
        }

        appt.TokenNumber = repo.GetLastToken(targetDate) + 1;
        appt.AppointmentDate = targetDate;
        appt.Status = "Pending";

        repo.Add(appt);
        return appt;
    }

}
