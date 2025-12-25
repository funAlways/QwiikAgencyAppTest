using System.Collections.Generic;
using System.Data.Entity;

public class AppDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AgencySetting> AgencySettings { get; set; }
    public DbSet<PublicHoliday> PublicHolidays { get; set; }

}

