using System;
using System.Collections.Generic;

namespace AR_System.Models;

public partial class Booking
{
    public int? UserId { get; set; }

    public int? ScheduleId { get; set; }

    public int? NoOfTickets { get; set; }

    public long? TotalPrice { get; set; }

    public string? TicketClass { get; set; }

    public long? PassportNumber { get; set; }

    public long? VisaNumber { get; set; }

    public string? PaymentMethod { get; set; }

    public int BookingId { get; set; }

    public virtual FlightSchedule? Schedule { get; set; }

    public virtual User? User { get; set; }
}
