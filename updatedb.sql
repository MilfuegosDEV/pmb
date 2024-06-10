CREATE DATABASE ganaderia;
USE ganaderia;


CREATE TABLE Departamento(
	departamentoId INT auto_increment PRIMARY KEY,
    name VARCHAR(60) NOT NULL UNIQUE
);

INSERT INTO Departamento(name) VALUES
	('PEON'),
    ('GANADERO'), 
    ('ADMINISTRACION');
    

CREATE TABLE RolEmpleado(
	rolEmpleadoId INT PRIMARY KEY,
    name VARCHAR(20) NOT NULL UNIQUE
);

INSERT INTO RolEmpleado (rolEmpleadoId, name)
	VALUES 
		(1,'ADMIN'),
        (2,'MODERATOR'),
        (3,'USER');



CREATE TABLE Empleado( 
	empleadoId INT AUTO_INCREMENT PRIMARY KEY,
    cedEmpleado VARCHAR(25) NOT NULL UNIQUE,
    nombre VARCHAR(60) NOT NULL,
    apellido VARCHAR(60) NOT NULL,
    nacionalidad VARCHAR(60) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password VARCHAR(255) NOT NULL,
    fechaNacimiento DATE NOT NULL,
    provincia VARCHAR(30) NOT NULL,
    canton VARCHAR(30) NOT NULL,
    distrito VARCHAR(30) NOT NULL,
    fechaIngreso DATE NOT NULL,
	salario DECIMAL (15, 2) NOT NULL,
    habilitado BOOL DEFAULT TRUE NOT NULL,
    departamentoId INT NOT NULL,
    roleEmpleadoId INT DEFAULT 3,
    FOREIGN KEY (departamentoId) REFERENCES Departamento(departamentoId),
    FOREIGN KEY (roleEmpleadoId) REFERENCES RolEmpleado(rolEmpleadoId)
);
