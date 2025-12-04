using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiHospital.Models;


namespace ApiHospital.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class AuthController : ControllerBase
     {
         [HttpPost("login")]
         public IActionResult Login(LoginDto login)
         {
             // Credenciales de ejemplo
             if (login.Usuario != "admin" || login.Password != "1234")
                 return Unauthorized(new { message = "Credenciales incorrectas" });
  
             var claims = new[]
             {
                 new Claim("usuario", login.Usuario),
                 new Claim(ClaimTypes.Role, "Admin")
             };
  
             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EstaEsUnaClaveSuperSeguraDe32Caracteres123!"));
             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
  
             var token = new JwtSecurityToken(
                                                issuer: "tu-api",
                                                audience: "tu-api",
                                                claims: claims,
                                                expires: DateTime.Now.AddHours(2),
                                                signingCredentials: creds
             );
  
             return Ok(new
             {
                 token = new JwtSecurityTokenHandler().WriteToken(token)
             });
         }
     }
}
