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
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired();
        
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