 CREATE DATABASE HospitalDB;
 GO
 USE HospitalDB;
 GO
  
 -- Tabla Paciente
 CREATE TABLE Paciente (
     Id INT IDENTITY(1,1) PRIMARY KEY,
     Nombre NVARCHAR(100) NOT NULL,
     Apellido NVARCHAR(100) NOT NULL,
     FechaNacimiento DATE NOT NULL,
     Telefono NVARCHAR(15) NULL
 );
  
 -- Tabla Doctor
 CREATE TABLE Doctor (
     Id INT IDENTITY(1,1) PRIMARY KEY,
     Nombre NVARCHAR(100) NOT NULL,
     Especialidad NVARCHAR(100) NOT NULL
 );
  
 -- Tabla Cita
 CREATE TABLE Cita (
     Id INT IDENTITY(1,1) PRIMARY KEY,
     PacienteId INT NOT NULL,
     DoctorId INT NOT NULL,
     Fecha DATETIME NOT NULL,
  
     CONSTRAINT FK_Cita_Paciente FOREIGN KEY (PacienteId)
         REFERENCES Paciente(Id),
  
     CONSTRAINT FK_Cita_Doctor FOREIGN KEY (DoctorId)
         REFERENCES Doctor(Id)
 );
