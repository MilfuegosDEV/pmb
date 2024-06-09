using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoMancariBlue.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<RolEmpleado> RolEmpleados { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("departamento");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.DepartamentoId).HasColumnName("departamentoId");

                entity.Property(e => e.Name)
                    .HasMaxLength(60)
                    .HasColumnName("name");
            });


            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("empleado");

                entity.HasIndex(e => e.CedEmpleado, "cedEmpleado")
                    .IsUnique();

                entity.HasIndex(e => e.DepartamentoId, "departamentoId");

                entity.HasIndex(e => e.RoleEmpleadoId, "roleEmpleadoId");

                entity.Property(e => e.EmpleadoId).HasColumnName("empleadoId");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(60)
                    .HasColumnName("apellido");

                entity.Property(e => e.Canton)
                    .HasMaxLength(30)
                    .HasColumnName("canton");

                entity.Property(e => e.CedEmpleado)
                    .HasMaxLength(25)
                    .HasColumnName("cedEmpleado");

                entity.Property(e => e.DepartamentoId).HasColumnName("departamentoId");

                entity.Property(e => e.Distrito)
                    .HasMaxLength(30)
                    .HasColumnName("distrito");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FechaIngreso).HasColumnName("fechaIngreso");

                entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasColumnName("habilitado")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Nacionalidad)
                    .HasMaxLength(60)
                    .HasColumnName("nacionalidad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(60)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .HasColumnType("string")
                    .HasColumnName("password");

                entity.Property(e => e.Provincia)
                    .HasMaxLength(30)
                    .HasColumnName("provincia");

                entity.Property(e => e.RoleEmpleadoId).HasColumnName("roleEmpleadoId");

                entity.Property(e => e.Salario)
                    .HasPrecision(15, 2)
                    .HasColumnName("salario");

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.DepartamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("empleado_ibfk_1");

                entity.HasOne(d => d.RoleEmpleado)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.RoleEmpleadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("empleado_ibfk_2");
            });


            modelBuilder.Entity<RolEmpleado>(entity =>
            {
                entity.HasKey(e => e.RolEmpleadoId)
                    .HasName("PRIMARY");

                entity.ToTable("rolempleado");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.RolEmpleadoId)
                    .ValueGeneratedNever()
                    .HasColumnName("rolEmpleadoId");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
