using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultaMedica.Migrations
{
    public partial class CrearData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "edad",
                table: "Paciente",
                newName: "Edad");

            migrationBuilder.AddColumn<string>(
                name: "HospitalDeTrabajo",
                table: "Doctor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalDeTrabajo",
                table: "Doctor");

            migrationBuilder.RenameColumn(
                name: "Edad",
                table: "Paciente",
                newName: "edad");
        }
    }
}
