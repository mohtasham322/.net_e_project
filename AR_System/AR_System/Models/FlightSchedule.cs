using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static AR_System.Models.FutureDateAttribute;

namespace AR_System.Models;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }
        return false;
    }
    public class FutureDateRelativeToDepartureAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public FutureDateRelativeToDepartureAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
            {
                return new ValidationResult($"Unknown property {_comparisonProperty}");
            }

            var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);

            if (value is DateTime arrivalTime && comparisonValue is DateTime departureTime)
            {
                if (arrivalTime > departureTime)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage ?? "Arrival time must be in the future compared to Departure time");
        }
    }
}
public partial class FlightSchedule
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Flight number is required")]
    public int? FlightId { get; set; }
    [Required(ErrorMessage = "Arrival location is required")]
    public int? ToCityId { get; set; }
    [Required(ErrorMessage = "Departure location is required")]
    public int? FromCityId { get; set; }
    [Required(ErrorMessage = "Departure time is required")]
    [FutureDate(ErrorMessage = "Departure time must be in the future")]
    public DateTime? DepartureTime { get; set; }
    [Required(ErrorMessage = "Arrival time is required")]
    [FutureDateRelativeToDeparture("DepartureTime", ErrorMessage = "Arrival time must be in the future compared to Departure time")]
    public DateTime? ArrivalTime { get; set; }
    [Required(ErrorMessage = "Stops are required if no, insert 0")]
    public int? Stops { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Flight? Flight { get; set; }

    public virtual CityWay? FromCity { get; set; }

    public virtual ICollection<TicketClass> TicketClasses { get; set; } = new List<TicketClass>();

    public virtual CityWay? ToCity { get; set; }
}
