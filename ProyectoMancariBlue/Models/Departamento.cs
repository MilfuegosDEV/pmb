using System;
using System.Collections.Generic;

namespace ProyectoMancariBlue.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int DepartamentoId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
