using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoMancariBlue.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "estado_empleado",
                columns: table => new
                {
                    idEstadoEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idEstadoEmpleado);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "rol_empleado",
                columns: table => new
                {
                    idRolEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rol = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idRolEmpleado);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "empleado",
                columns: table => new
                {
                    idEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<decimal>(type: "decimal(8)", precision: 8, nullable: false),
                    correoElectronico = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contrasena = table.Column<byte[]>(type: "blob", nullable: false),
                    idEstadoEmpleado = table.Column<int>(type: "int", nullable: false),
                    idRolEmpleado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idEmpleado);
                    table.ForeignKey(
                        name: "empleado_ibfk_1",
                        column: x => x.idEstadoEmpleado,
                        principalTable: "estado_empleado",
                        principalColumn: "idEstadoEmpleado");
                    table.ForeignKey(
                        name: "empleado_ibfk_2",
                        column: x => x.idRolEmpleado,
                        principalTable: "rol_empleado",
                        principalColumn: "idRolEmpleado");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "correoElectronico",
                table: "empleado",
                column: "correoElectronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idEstadoEmpleado",
                table: "empleado",
                column: "idEstadoEmpleado");

            migrationBuilder.CreateIndex(
                name: "idRolEmpleado",
                table: "empleado",
                column: "idRolEmpleado");

            migrationBuilder.CreateIndex(
                name: "telefono",
                table: "empleado",
                column: "telefono",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empleado");

            migrationBuilder.DropTable(
                name: "estado_empleado");

            migrationBuilder.DropTable(
                name: "rol_empleado");
        }
    }
}
