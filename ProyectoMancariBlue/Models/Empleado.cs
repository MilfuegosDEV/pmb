using System;
using System.Collections.Generic;

namespace ProyectoMancariBlue.Models
{
    public partial class Empleado
    {
        public int EmpleadoId { get; set; }
        public string CedEmpleado { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Nacionalidad { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public DateOnly FechaNacimiento { get; set; }
        public string Provincia { get; set; } = null!;
        public string Canton { get; set; } = null!;
        public string Distrito { get; set; } = null!;
        public DateOnly FechaIngreso { get; set; }
        public decimal Salario { get; set; }
        public bool? Habilitado { get; set; }
        public int DepartamentoId { get; set; }
        public int RoleEmpleadoId { get; set; }

        public virtual Departamento Departamento { get; set; } = null!;
        public virtual RolEmpleado RoleEmpleado { get; set; } = null!;
    }
}
