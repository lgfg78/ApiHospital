using ApiHospital.Data;
using ApiHospital.DTOs;
using ApiHospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiHospital.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class PacientesController : ControllerBase
     {
         private readonly HospitalContext _context;
  
         public PacientesController(HospitalContext context)
         {
             _context = context;
         }

        // GET: api/pacientes
        [Authorize]
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
         {
             return await _context.Pacientes.ToListAsync();
         }

         // GET: api/pacientes/5
         [HttpGet("{id}")]
         public async Task<ActionResult<Paciente>> GetPaciente(int id)
         {
            var paciente = await _context.Pacientes.FindAsync(id);
    
             if (paciente == null) return NotFound();
    
             return paciente;
        }


        // POST: api/pacientes
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(CrearPacienteDto pacienteDto)
        {
            // Mapeamos manualmente el DTO a la Entidad
            var paciente = new Paciente
            {
                Nombre = pacienteDto.Nombre,
                Apellido = pacienteDto.Apellido,
                FechaNacimiento = pacienteDto.FechaNacimiento,
                Telefono = pacienteDto.Telefono
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaciente", new { id = paciente.Id }, paciente);
        }


        // PUT: api/pacientes/5
        [HttpPut("{id}")]
         public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
         {
            if (id != paciente.Id) return BadRequest("El ID no coincide.");
    
            _context.Entry(paciente).State = EntityState.Modified;
            try
              {
                 await _context.SaveChangesAsync();
               }
                 catch (DbUpdateConcurrencyException)
                  {
                     if (!_context.Pacientes.Any(e => e.Id == id))  return NotFound();
                         else throw;
                  }
    
                 return NoContent();
         }
    
        // DELETE: api/pacientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
         {
            var paciente = await _context.Pacientes.FindAsync(id);
    
            if (paciente == null) return NotFound();
    
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
    
            return NoContent();
         }
     }

}
