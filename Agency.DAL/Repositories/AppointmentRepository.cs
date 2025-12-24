using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        AppDbContext db = new AppDbContext();

        public int GetDailyCount(DateTime date)
        {
            return db.Appointments.Count(a =>
                DbFunctions.TruncateTime(a.AppointmentDate) == date);
        }

        public int GetLastToken(DateTime date)
        {
            return db.Appointments
                .Where(a => DbFunctions.TruncateTime(a.AppointmentDate) == date)
                .OrderByDescending(a => a.TokenNumber)
                .Select(a => (int?)a.TokenNumber)
                .FirstOrDefault() ?? 0;
        }

        public void Add(Appointment appt)
        {
            db.Appointments.Add(appt);
            db.SaveChanges();
        }
    }

}
