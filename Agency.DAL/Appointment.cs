using System;

public class Appointment
{
    public int AppointmentId { get; set; }
    public string CustomerName { get; set; }
    public string Phone { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int TokenNumber { get; set; }
    public string Status { get; set; }   // Pending, Completed, Skipped
}
