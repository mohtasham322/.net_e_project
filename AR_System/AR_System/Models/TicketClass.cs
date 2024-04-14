using System;
using System.Collections.Generic;

namespace AR_System.Models;

public partial class TicketClass
{
    public int Id { get; set; }

    public int? ScheduleId { get; set; }

    public decimal? EconomyPrice { get; set; }

    public decimal? FirstClassPrice { get; set; }

    public decimal? BusinessPrice { get; set; }

    public virtual FlightSchedule? Schedule { get; set; }
}
