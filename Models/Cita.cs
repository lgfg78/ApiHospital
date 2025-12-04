namespace ApiHospital.Models
 {
     public class Cita
     {
         public int Id { get; set; }
         public int PacienteId { get; set; }
         public int DoctorId { get; set; }
         public DateTime Fecha { get; set; }
  
         // Relaciones
         public Paciente Paciente { get; set; }
         public Doctor Doctor { get; set; }
     }
 }
