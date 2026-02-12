CREATE DATABASE Task
USE Task

drop TABLE Tarea

CREATE TABLE Tarea (
	IdTarea BIGINT IDENTITY(1,1) CONSTRAINT PK_Tareas PRIMARY KEY,  
	NombreTarea NVARCHAR(100) NOT NULL,
	DescripcionTarea NVARCHAR(MAX),
 	Prioridad NVARCHAR(20) NOT NULL CONSTRAINT PrioridadK_Estatus CHECK (Prioridad IN ('Alta', 'Media', 'Baja')),
	Estatus NVARCHAR (20) NOT NULL DEFAULT 'Pendiente' CONSTRAINT EstatusK CHECK (Estatus in ('Pendiente', 'Completado')),
	FechaRegistro DATETIME2 NOT NULL CONSTRAINT DF_FechaTarea DEFAULT GETDATE()
);