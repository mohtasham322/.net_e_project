using System;
using System.Collections.Generic;

namespace AR_System.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public long? PhoneNumber { get; set; }

    public string? Sex { get; set; }

    public int? Age { get; set; }

    public int? SkyMiles { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Role? Role { get; set; }
}
