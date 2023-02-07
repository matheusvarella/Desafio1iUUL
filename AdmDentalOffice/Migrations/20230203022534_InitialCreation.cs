using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmDentalOffice.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Cpf = table.Column<int>(type: "INT", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "SMALLDATETIME", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "INT", nullable: false),
                    Cpf = table.Column<int>(type: "INT", nullable: false),
                    AppointmentDate = table.Column<string>(type: "NVARCHAR(10)", maxLength: 10, nullable: false),
                    StartTime = table.Column<string>(type: "NVARCHAR(4)", maxLength: 4, nullable: false),
                    EndTime = table.Column<string>(type: "NVARCHAR(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Appointment",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
