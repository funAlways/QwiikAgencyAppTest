using System.Collections.Generic;
using System.Data.Entity;

namespace QwiikMVC.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AgencySetting> AgencySettings { get; set; }

    }

}
