using System;
using System.Collections.Generic;

namespace ProyectoMancariBlue.Models
{
    public partial class EstadoEmpleado
    {
        public EstadoEmpleado()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdEstadoEmpleado { get; set; }
        public string Estado { get; set; } = null!;

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
