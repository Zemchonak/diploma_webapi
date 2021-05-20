using System;
using FitnessCenterManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterManagement.DataAccess.Contexts
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            Identity = Guid.NewGuid();
        }

        public Guid Identity { get; }

        public DbSet<Abonement> Abonements { get; set; }

        public DbSet<CustomerCategory> CustomerCategories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Review> Review { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<AbonementFitnessEvent> AbonementFitnessEvents { get; set; }

        public DbSet<FitnessEvent> FitnessEvents { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Entities.Message> Messages { get; set; }

        public DbSet<WeeklyEvent> WeeklyEvents { get; set; }

        public DbSet<CardEventItem> CardEventItems { get; set; }

        public DbSet<DateEvent> DateEvents { get; set; }

        public DbSet<AbonementCard> AbonementCards { get; set; }
    }
}
