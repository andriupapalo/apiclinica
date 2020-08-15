using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultaMedica.Migrations
{
    public partial class CrearData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Paciente",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Paciente");
        }
    }
}
