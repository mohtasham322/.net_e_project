using System;
using System.Collections.Generic;
using AR_System.Models;
using Microsoft.EntityFrameworkCore;

namespace AR_System.Data;

public partial class AirlineReservationSystemContext : DbContext
{
    public AirlineReservationSystemContext()
    {
    }

    public AirlineReservationSystemContext(DbContextOptions<AirlineReservationSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<CityWay> CityWays { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightSchedule> FlightSchedules { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TicketClass> TicketClasses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Initial Catalog=airline_reservation_system;Persist Security Info=False;User ID=mohtasham;Password=hellodev;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__booking__5DE3A5B1967A2637");

            entity.ToTable("booking");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.NoOfTickets).HasColumnName("no_of_tickets");
            entity.Property(e => e.PassportNumber).HasColumnName("passport_number");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.TicketClass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ticket_class");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.VisaNumber).HasColumnName("visa_number");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("fk_schedule_id");
        });

        modelBuilder.Entity<CityWay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__route__3213E83F2E4916A2");

            entity.ToTable("city_way");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city_name");
            entity.Property(e => e.PortName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("port_name");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__flight__3213E83F2BA39317");

            entity.ToTable("flight");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("flight_number");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<FlightSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__flight_s__3213E83F142AF127");

            entity.ToTable("flight_schedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArrivalTime)
                .HasColumnType("datetime")
                .HasColumnName("arrival_time");
            entity.Property(e => e.DepartureTime)
                .HasColumnType("datetime")
                .HasColumnName("departure_time");
            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.FromCityId).HasColumnName("from_city_id");
            entity.Property(e => e.Stops).HasColumnName("stops");
            entity.Property(e => e.ToCityId).HasColumnName("to_city_id");

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightSchedules)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("fk_flight_id");

            entity.HasOne(d => d.FromCity).WithMany(p => p.FlightScheduleFromCities)
                .HasForeignKey(d => d.FromCityId)
                .HasConstraintName("fk_from_route_id");

            entity.HasOne(d => d.ToCity).WithMany(p => p.FlightScheduleToCities)
                .HasForeignKey(d => d.ToCityId)
                .HasConstraintName("fk_to_route_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F7C8A8923");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TicketClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ticket_c__3213E83F3AA321D2");

            entity.ToTable("ticket_class");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("business_price");
            entity.Property(e => e.EconomyPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("economy_price");
            entity.Property(e => e.FirstClassPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("firstClass_price");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

            entity.HasOne(d => d.Schedule).WithMany(p => p.TicketClasses)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("fk_ticket_class_flight_schedule");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F12BB2787");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616487C7D340").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.RoleId)
                .HasDefaultValue(0)
                .HasColumnName("role_id");
            entity.Property(e => e.Sex)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sex");
            entity.Property(e => e.SkyMiles)
                .HasDefaultValue(0)
                .HasColumnName("sky_miles");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_role_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
