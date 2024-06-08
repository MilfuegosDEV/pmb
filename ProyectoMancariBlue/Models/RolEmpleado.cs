using System;
using System.Collections.Generic;

namespace ProyectoMancariBlue.Models
{
    public partial class RolEmpleado
    {
        public RolEmpleado()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int RolEmpleadoId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
