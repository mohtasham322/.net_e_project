using System;
using System.Collections.Generic;

namespace AR_System.Models;

public partial class FlightSchedule
{
    public int Id { get; set; }

    public int? FlightId { get; set; }

    public int? ToCityId { get; set; }

    public int? FromCityId { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public int? Stops { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Flight? Flight { get; set; }

    public virtual CityWay? FromCity { get; set; }

    public virtual ICollection<TicketClass> TicketClasses { get; set; } = new List<TicketClass>();

    public virtual CityWay? ToCity { get; set; }
}
