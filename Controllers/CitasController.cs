using ApiHospital.Data;
using ApiHospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiHospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
       private readonly HospitalContext _context;
       public CitasController(HospitalContext context)
       {
          _context = context;
       }
  
      // GET: api/citas
      [HttpGet]
      public async Task<ActionResult<IEnumerable<Cita>>> GetCitas()
      {
         return await _context.Citas.Include(c => c.Paciente)
                                                         .Include(c => c.Doctor)
                                                         .ToListAsync();
      }

      // GET: api/citas/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Cita>> GetCita(int id)
      {
         var cita = await _context.Citas
                    .Include(c => c.Paciente)
                    .Include(c => c.Doctor)
                    .FirstOrDefaultAsync(c => c.Id == id);
    
        if (cita == null) return NotFound();
    
       return cita;
      }

     // POST: api/citas
     [HttpPost]
     public async Task<ActionResult<Cita>> PostCita(Cita cita)
     {
        if (!_context.Pacientes.Any(p => p.Id == cita.PacienteId)) return BadRequest("El paciente no existe.");
    
        if (!_context.Doctores.Any(d => d.Id == cita.DoctorId)) return BadRequest("El doctor no existe.");
    
        _context.Citas.Add(cita);
        await _context.SaveChangesAsync();
    
        return CreatedAtAction("GetCita", new { id = cita.Id }, cita);
     }

    // PUT: api/citas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCita(int id, Cita cita)
    {
       if (id != cita.Id) return BadRequest("El ID no coincide.");
       _context.Entry(cita).State = EntityState.Modified;
       await _context.SaveChangesAsync();
       return NoContent();
    }

    // DELETE: api/citas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCita(int id)
    {
       var cita = await _context.Citas.FindAsync(id);
    
       if (cita == null) return NotFound();
    
       _context.Citas.Remove(cita);
       await _context.SaveChangesAsync();
             
       return NoContent();
    }
  }

}
