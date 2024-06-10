using System;
using System.Collections.Generic;

namespace ProyectoMancariBlue.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public partial class Empleado
    {
        public int EmpleadoId { get; set; }
        [Required(ErrorMessage = "La cédula es requerida")]
        [StringLength(maximumLength:25, ErrorMessage = "Se admiten máximo 25 caracteres.")]
        public string CedEmpleado { get; set; } = null!;
        [Required(ErrorMessage ="El nombre es requerido")]
        [StringLength(maximumLength:60, ErrorMessage ="Tamaño maximo 60 caracteres")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage ="El apellido es requerido")]
        [StringLength(maximumLength: 60, ErrorMessage = "Tamaño maximo 60 caracteres")]
        public string Apellido { get; set; } = null!;
        [Required(ErrorMessage ="La nacionalidad es requerida")]
        [StringLength(maximumLength: 60, ErrorMessage ="El tamaño máximo son 60 caracteres")]
        public string Nacionalidad { get; set; } = null!;
        [Required(ErrorMessage ="El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage ="Correo electrónico no válido")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public DateOnly FechaNacimiento { get; set; }
        [Required(ErrorMessage ="La provincia es requerida")]
        public string Provincia { get; set; } = null!;
        [Required(ErrorMessage ="El cantón es requerido")]
        public string Canton { get; set; } = null!;
        [Required(ErrorMessage ="El distrito es requerido")]
        public string Distrito { get; set; } = null!;
        [Required(ErrorMessage ="La fecha de ingreso es requerida")]
        public DateOnly FechaIngreso { get; set; }
        [Required(ErrorMessage ="El salario es requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El salario debe ser un número entero")]
        public decimal Salario { get; set; }
        public bool? Habilitado { get; set; }
        public int? DepartamentoId { get; set; }
        public int? RoleEmpleadoId { get; set; }

        public virtual Departamento Departamento { get; set; } = null!;
        public virtual RolEmpleado RoleEmpleado { get; set; } = null!;
    }
}
