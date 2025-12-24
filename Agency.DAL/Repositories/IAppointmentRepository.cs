using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Repositories
{
    public interface IAppointmentRepository
    {
        int GetDailyCount(DateTime date);
        int GetLastToken(DateTime date);
        void Add(Appointment appt);
    }

}
