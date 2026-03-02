namespace UbiquitousEngine.Api.Data;

using Microsoft.EntityFrameworkCore;
using UbiquitousEngine.Api.Models;

public class ServiceManagementContext : DbContext
{
    public ServiceManagementContext(DbContextOptions<ServiceManagementContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<Vehicle> Vehicles { get; set; }
    
    public DbSet<Part> Parts { get; set; }
    
    public DbSet<ServiceTicket> ServiceTickets { get; set; }
    
    public DbSet<ServiceTicketPart> ServiceTicketParts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Customer configuration
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);
            
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(500);
        });

        // Vehicle configuration
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.VIN)
                .IsRequired()
                .HasMaxLength(17);
            
            entity.Property(e => e.Make)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Model)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.LicensePlate)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.HasOne(e => e.Customer)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Part configuration
        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);
            
            entity.Property(e => e.PartNumber)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)");
        });

        // ServiceTicket configuration
        modelBuilder.Entity<ServiceTicket>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);
            
            entity.Property(e => e.LaborCost)
                .HasColumnType("decimal(18, 2)");
            
            entity.HasOne(e => e.Vehicle)
                .WithMany(v => v.ServiceTickets)
                .HasForeignKey(e => e.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ServiceTicketPart configuration
        modelBuilder.Entity<ServiceTicketPart>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.ServiceTicket)
                .WithMany(st => st.ServiceTicketParts)
                .HasForeignKey(e => e.ServiceTicketId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Part)
                .WithMany(p => p.ServiceTicketParts)
                .HasForeignKey(e => e.PartId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "555-0101",
                Address = "123 Main St, Springfield, IL 62701",
                CreatedAt = new DateTime(2026, 1, 15, 10, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 15, 10, 0, 0, DateTimeKind.Utc)
            },
            new Customer
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "555-0102",
                Address = "456 Oak Ave, Springfield, IL 62702",
                CreatedAt = new DateTime(2026, 1, 20, 14, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 20, 14, 30, 0, DateTimeKind.Utc)
            },
            new Customer
            {
                Id = 3,
                FirstName = "Robert",
                LastName = "Johnson",
                Email = "robert.j@example.com",
                PhoneNumber = "555-0103",
                Address = "789 Pine Rd, Springfield, IL 62703",
                CreatedAt = new DateTime(2026, 2, 1, 9, 15, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 9, 15, 0, DateTimeKind.Utc)
            }
        );

        // Seed Vehicles
        modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle
            {
                Id = 1,
                VIN = "1HGBH41JXMN109186",
                Make = "Honda",
                Model = "Accord",
                Year = 2020,
                LicensePlate = "ABC-1234",
                CustomerId = 1,
                CreatedAt = new DateTime(2026, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 15, 10, 30, 0, DateTimeKind.Utc)
            },
            new Vehicle
            {
                Id = 2,
                VIN = "2T1BURHE0JC123456",
                Make = "Toyota",
                Model = "Camry",
                Year = 2021,
                LicensePlate = "XYZ-5678",
                CustomerId = 2,
                CreatedAt = new DateTime(2026, 1, 20, 15, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 20, 15, 0, 0, DateTimeKind.Utc)
            },
            new Vehicle
            {
                Id = 3,
                VIN = "3FA6P0HD5JR123789",
                Make = "Ford",
                Model = "Fusion",
                Year = 2019,
                LicensePlate = "DEF-9012",
                CustomerId = 1,
                CreatedAt = new DateTime(2026, 2, 5, 11, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 5, 11, 0, 0, DateTimeKind.Utc)
            },
            new Vehicle
            {
                Id = 4,
                VIN = "1G1ZD5ST8LF123456",
                Make = "Chevrolet",
                Model = "Malibu",
                Year = 2022,
                LicensePlate = "GHI-3456",
                CustomerId = 3,
                CreatedAt = new DateTime(2026, 2, 1, 10, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 10, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed Parts
        modelBuilder.Entity<Part>().HasData(
            new Part
            {
                Id = 1,
                Name = "Oil Filter",
                PartNumber = "OF-12345",
                Price = 12.99m,
                QuantityInStock = 50,
                CreatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc)
            },
            new Part
            {
                Id = 2,
                Name = "Air Filter",
                PartNumber = "AF-54321",
                Price = 18.50m,
                QuantityInStock = 40,
                CreatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc)
            },
            new Part
            {
                Id = 3,
                Name = "Brake Pads (Front)",
                PartNumber = "BP-11111",
                Price = 65.00m,
                QuantityInStock = 25,
                CreatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc)
            },
            new Part
            {
                Id = 4,
                Name = "Brake Pads (Rear)",
                PartNumber = "BP-22222",
                Price = 55.00m,
                QuantityInStock = 25,
                CreatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc)
            },
            new Part
            {
                Id = 5,
                Name = "Spark Plugs (Set of 4)",
                PartNumber = "SP-99999",
                Price = 32.00m,
                QuantityInStock = 30,
                CreatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc)
            },
            new Part
            {
                Id = 6,
                Name = "Windshield Wipers",
                PartNumber = "WW-77777",
                Price = 24.99m,
                QuantityInStock = 45,
                CreatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc)
            },
            new Part
            {
                Id = 7,
                Name = "Battery",
                PartNumber = "BAT-88888",
                Price = 125.00m,
                QuantityInStock = 15,
                CreatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 8, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed Service Tickets
        modelBuilder.Entity<ServiceTicket>().HasData(
            new ServiceTicket
            {
                Id = 1,
                VehicleId = 1,
                Description = "Regular oil change and filter replacement",
                Status = ServiceStatus.Completed,
                LaborCost = 35.00m,
                CreatedAt = new DateTime(2026, 2, 10, 9, 0, 0, DateTimeKind.Utc),
                CompletedAt = new DateTime(2026, 2, 10, 10, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 10, 10, 30, 0, DateTimeKind.Utc)
            },
            new ServiceTicket
            {
                Id = 2,
                VehicleId = 2,
                Description = "Replace front and rear brake pads",
                Status = ServiceStatus.Completed,
                LaborCost = 120.00m,
                CreatedAt = new DateTime(2026, 2, 15, 10, 0, 0, DateTimeKind.Utc),
                CompletedAt = new DateTime(2026, 2, 15, 14, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 15, 14, 0, 0, DateTimeKind.Utc)
            },
            new ServiceTicket
            {
                Id = 3,
                VehicleId = 3,
                Description = "Battery replacement and electrical system check",
                Status = ServiceStatus.InProgress,
                LaborCost = 45.00m,
                CreatedAt = new DateTime(2026, 3, 1, 8, 30, 0, DateTimeKind.Utc),
                CompletedAt = null,
                UpdatedAt = new DateTime(2026, 3, 1, 8, 30, 0, DateTimeKind.Utc)
            },
            new ServiceTicket
            {
                Id = 4,
                VehicleId = 4,
                Description = "Annual maintenance: oil change, air filter, spark plugs",
                Status = ServiceStatus.Open,
                LaborCost = 75.00m,
                CreatedAt = new DateTime(2026, 3, 2, 7, 0, 0, DateTimeKind.Utc),
                CompletedAt = null,
                UpdatedAt = new DateTime(2026, 3, 2, 7, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed Service Ticket Parts
        modelBuilder.Entity<ServiceTicketPart>().HasData(
            // Service Ticket 1 parts (Oil change)
            new ServiceTicketPart { Id = 1, ServiceTicketId = 1, PartId = 1, Quantity = 1 },
            
            // Service Ticket 2 parts (Brake job)
            new ServiceTicketPart { Id = 2, ServiceTicketId = 2, PartId = 3, Quantity = 1 },
            new ServiceTicketPart { Id = 3, ServiceTicketId = 2, PartId = 4, Quantity = 1 },
            
            // Service Ticket 3 parts (Battery replacement)
            new ServiceTicketPart { Id = 4, ServiceTicketId = 3, PartId = 7, Quantity = 1 },
            
            // Service Ticket 4 parts (Annual maintenance)
            new ServiceTicketPart { Id = 5, ServiceTicketId = 4, PartId = 1, Quantity = 1 },
            new ServiceTicketPart { Id = 6, ServiceTicketId = 4, PartId = 2, Quantity = 1 },
            new ServiceTicketPart { Id = 7, ServiceTicketId = 4, PartId = 5, Quantity = 1 }
        );
    }
}
