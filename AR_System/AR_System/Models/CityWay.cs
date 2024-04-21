using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AR_System.Models;

public partial class CityWay
{
    public int Id { get; set; }
    [Required(ErrorMessage = "city name is required")]
    public string CityName { get; set; } = null!;
    [Required(ErrorMessage = "port name is required")]
    public string PortName { get; set; } = null!;

    public virtual ICollection<FlightSchedule> FlightScheduleFromCities { get; set; } = new List<FlightSchedule>();

    public virtual ICollection<FlightSchedule> FlightScheduleToCities { get; set; } = new List<FlightSchedule>();
}
