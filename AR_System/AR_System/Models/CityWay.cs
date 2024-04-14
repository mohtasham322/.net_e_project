using System;
using System.Collections.Generic;

namespace AR_System.Models;

public partial class CityWay
{
    public int Id { get; set; }

    public string CityName { get; set; } = null!;

    public string PortName { get; set; } = null!;

    public virtual ICollection<FlightSchedule> FlightScheduleFromCities { get; set; } = new List<FlightSchedule>();

    public virtual ICollection<FlightSchedule> FlightScheduleToCities { get; set; } = new List<FlightSchedule>();
}
