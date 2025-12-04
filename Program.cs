using ApiHospital.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// **PASO 1: Ajuste de la Clave Secreta en la Configuración (Importante)**
// Se recomienda mover la clave a appsettings.json y obtenerla desde allí.
// Por ejemplo, en appsettings.json: "JwtSettings": { "SecretKey": "TuClaveSecretaDe256BitsOMas..." }
// Para el ejemplo, usaremos el mismo método, pero nos aseguramos de que sea más larga.

// --- Servicios ---
builder.Services.AddDbContext<HospitalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
        options.TokenValidationParameters = new TokenValidationParameters
         {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "tu-api",
                ValidAudience = "tu-api",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("EstaEsUnaClaveSuperSeguraDe32Caracteres123!")
                )
         };
     });


builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    // ... otras configuraciones

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa el token Bearer de la siguiente manera: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.MapOpenApi();
    app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHospital v1");
    });

}


app.UseHttpsRedirection();

// Importante: El orden es clave. UseAuthentication debe ir antes de UseAuthorization.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

