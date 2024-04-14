using System;
using System.Collections.Generic;

namespace AR_System.Models;

public partial class Flight
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? FlightNumber { get; set; }

    public virtual ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();
}
