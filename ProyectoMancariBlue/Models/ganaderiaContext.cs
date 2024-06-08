using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoMancariBlue.Models
{
    public partial class ganaderiaContext : DbContext
    {
        public ganaderiaContext()
        {
        }

        public ganaderiaContext(DbContextOptions<ganaderiaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<EstadoEmpleado> EstadoEmpleados { get; set; } = null!;
        public virtual DbSet<RolEmpleado> RolEmpleados { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PRIMARY");

                entity.ToTable("empleado");

                entity.HasIndex(e => e.CorreoElectronico, "correoElectronico")
                    .IsUnique();

                entity.HasIndex(e => e.IdEstadoEmpleado, "idEstadoEmpleado");

                entity.HasIndex(e => e.IdRolEmpleado, "idRolEmpleado");

                entity.HasIndex(e => e.Telefono, "telefono")
                    .IsUnique();

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("apellido");

                entity.Property(e => e.Contrasena)
                    .HasColumnType("blob")
                    .HasColumnName("contrasena");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .HasColumnName("correoElectronico");

                entity.Property(e => e.IdEstadoEmpleado).HasColumnName("idEstadoEmpleado");

                entity.Property(e => e.IdRolEmpleado).HasColumnName("idRolEmpleado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasPrecision(8)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdEstadoEmpleadoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdEstadoEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("empleado_ibfk_1");

                entity.HasOne(d => d.IdRolEmpleadoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdRolEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("empleado_ibfk_2");
            });

            modelBuilder.Entity<EstadoEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdEstadoEmpleado)
                    .HasName("PRIMARY");

                entity.ToTable("estado_empleado");

                entity.Property(e => e.IdEstadoEmpleado).HasColumnName("idEstadoEmpleado");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasColumnName("estado");
            });

            modelBuilder.Entity<RolEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdRolEmpleado)
                    .HasName("PRIMARY");

                entity.ToTable("rol_empleado");

                entity.Property(e => e.IdRolEmpleado).HasColumnName("idRolEmpleado");

                entity.Property(e => e.Rol)
                    .HasMaxLength(15)
                    .HasColumnName("rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
