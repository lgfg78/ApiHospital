 INSERT INTO Paciente (Nombre, Apellido, FechaNacimiento, Telefono)
 VALUES ('Carlos', 'Ramírez', '1990-05-12', '4491002030');
  
 INSERT INTO Doctor (Nombre, Especialidad)
 VALUES ('Dra. Patricia López', 'Cardiología'),
        ('Dr. Hernán Ruiz', 'Pediatría');
  
 INSERT INTO Cita (PacienteId, DoctorId, Fecha)
 VALUES (1, 1, '2025-02-01 10:00'),
        (1, 2, '2025-02-10 15:30');
