using System;
using System.Collections.Generic;

namespace ProyectoMancariBlue.Models
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public decimal Telefono { get; set; }
        public string CorreoElectronico { get; set; } = null!;
        public byte[] Contrasena { get; set; } = null!;
        public int IdEstadoEmpleado { get; set; }
        public int IdRolEmpleado { get; set; }

        public virtual EstadoEmpleado IdEstadoEmpleadoNavigation { get; set; } = null!;
        public virtual RolEmpleado IdRolEmpleadoNavigation { get; set; } = null!;
    }
}
