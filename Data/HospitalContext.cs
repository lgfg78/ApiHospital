 using Microsoft.EntityFrameworkCore;
 using ApiHospital.Models;
  
 namespace ApiHospital.Data
 {
     public class HospitalContext : DbContext
     {
         public HospitalContext(DbContextOptions<HospitalContext> options)
             : base(options)
         {
         }
  
         // Tablas
         public DbSet<Paciente> Pacientes { get; set; }
         public DbSet<Doctor> Doctores { get; set; }
         public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>().ToTable("Paciente"); // Fuerza el nombre singular
            modelBuilder.Entity<Doctor>().ToTable("Doctor"); // Fuerza el nombre singular
            modelBuilder.Entity<Cita>().ToTable("Cita"); // Fuerza el nombre singular
        }
    }
 }
