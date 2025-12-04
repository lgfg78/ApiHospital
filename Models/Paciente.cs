namespace ApiHospital.Models
 {
     public class Paciente
     {
         public int Id { get; set; }
         public string Nombre { get; set; }
         public string Apellido { get; set; }
         public DateTime FechaNacimiento { get; set; }
         public string Telefono { get; set; }
     }
 }
