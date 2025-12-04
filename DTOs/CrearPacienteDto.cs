using System.ComponentModel.DataAnnotations;

namespace ApiHospital.DTOs
{
    public class CrearPacienteDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        public string Telefono { get; set; }
    }

}
