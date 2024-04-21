using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AR_System.Models;

public partial class Booking
{
    public int? ScheduleId { get; set; }
    [Required]
    public int? NoOfTickets { get; set; }

    public long? TotalPrice { get; set; }

    public string? TicketClass { get; set; }
    [Required]
    public long? PassportNumber { get; set; }
    [Required]
    public long? VisaNumber { get; set; }

    public string? PaymentMethod { get; set; }

    public int BookingId { get; set; }

    public string? UserEmail { get; set; }

    public virtual FlightSchedule? Schedule { get; set; }
}
