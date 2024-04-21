using AR_System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AR_System.Models;
public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var emailAddressAttribute = new EmailAddressAttribute();
        if (!emailAddressAttribute.IsValid(value))
        {
            return new ValidationResult("Invalid email format");
        }

        var dbContext = (AirlineReservationSystemContext)validationContext.GetService(typeof(AirlineReservationSystemContext));

        if (dbContext.Users.Any(u => u.Email == (string)value))
        {
            return new ValidationResult("Email already exists");
        }

        return ValidationResult.Success;
    }
}
public partial class User
{
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 8 characters long.")]
    public string? Password { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must contain 10 characters .")]
    public long? PhoneNumber { get; set; }

    public string? Sex { get; set; }
    [Required]
    [RegularExpression(@"^\d{2}$", ErrorMessage = "Age must contain 2 characters")]
    public int? Age { get; set; }

    public int? SkyMiles { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
