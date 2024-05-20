using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.Extensions.Configuration;


// ein Context pro Datenbank => muss in Startup.cs in ConfigureServices hinzugefuegt werden

namespace backend.Data
{
  public class FeedingContext : DbContext
  {
    
    protected readonly IConfiguration Configuration;
    public FeedingContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // ein DbSet pro Model/Tabelle => Grundlage fuer Queries
    public DbSet<Feeding> Feedings { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // Modelle ihren Tabellen zuordnen
       modelBuilder.Entity<Feeding>().ToTable("feeding");    
       modelBuilder.Entity<Brand>().ToTable("stamm_brand");
       modelBuilder.Entity<Product>().ToTable("stamm_product");    

      // EF setzt initial Zeitzone, muss angepasst werden wenn keine in DB angegeben ist 
      foreach (var property in modelBuilder.Model.GetEntityTypes()
                 .SelectMany(t => t.GetProperties())
                 .Where( p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?) )
        )
      {
         property.SetColumnType("timestamp without time zone");
      }  
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      // Verbindung zur Datenbank herstellen     

      // https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
      optionsBuilder.UseNpgsql(Configuration.GetConnectionString("FeedingDatabase"));
      
    } 

  }
}