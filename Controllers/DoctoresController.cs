using ApiHospital.Data;
using ApiHospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;


namespace ApiHospital.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
      public class DoctoresController : ControllerBase
      {
         private readonly HospitalContext _context;
  
         public DoctoresController(HospitalContext context)
         {
             _context = context;
         }
  
         [HttpGet]
         public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctores()
         {
             return await _context.Doctores.ToListAsync();
         }
  
         [HttpGet("{id}")]
         public async Task<ActionResult<Doctor>> GetDoctor(int id)
         {
             var doctor = await _context.Doctores.FindAsync(id);
  
             if (doctor == null) return NotFound();
  
             return doctor;
         }

         [HttpPost]
         public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
         {
             _context.Doctores.Add(doctor);
             await _context.SaveChangesAsync();
    
             return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
         }

         [HttpPut("{id}")]
         public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
         {
            if (id != doctor.Id) return BadRequest("El ID no coincide.");
    
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
    
            return NoContent();
         }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
           var doctor = await _context.Doctores.FindAsync(id);
     
           if (doctor == null) return NotFound();
    
           _context.Doctores.Remove(doctor);
           await _context.SaveChangesAsync();
    
           return NoContent();
        }
      }


}
