using LarTechPersons.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LarTechPersons.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Telephone> Telephones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasKey(p => p.Id);
        
        modelBuilder.Entity<Telephone>()
            .HasKey(t => t.Id);
        
        modelBuilder.Entity<Telephone>()
            .HasOne<Person>()
            .WithMany(p => p.Telephones)
            .HasForeignKey(t => t.PersonId)
            .IsRequired(false);
    }
}