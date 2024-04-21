using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AR_System.Models;

public partial class Flight
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Flight name is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Flight number is required")]
    public string? FlightNumber { get; set; }

    public virtual ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();
}
