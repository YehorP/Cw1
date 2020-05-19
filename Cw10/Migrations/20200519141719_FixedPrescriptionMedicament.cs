using Microsoft.EntityFrameworkCore.Migrations;

namespace Cw10.Migrations
{
    public partial class FixedPrescriptionMedicament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription_Mdedicament",
                table: "Prescription_Mdedicament");

            migrationBuilder.RenameTable(
                name: "Prescription_Mdedicament",
                newName: "Prescription_Medicament");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_Mdedicament_IdMedicament",
                table: "Prescription_Medicament",
                newName: "IX_Prescription_Medicament_IdMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription_Medicament",
                table: "Prescription_Medicament",
                columns: new[] { "IdPrescription", "IdMedicament" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription_Medicament",
                table: "Prescription_Medicament");

            migrationBuilder.RenameTable(
                name: "Prescription_Medicament",
                newName: "Prescription_Mdedicament");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_Medicament_IdMedicament",
                table: "Prescription_Mdedicament",
                newName: "IX_Prescription_Mdedicament_IdMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription_Mdedicament",
                table: "Prescription_Mdedicament",
                columns: new[] { "IdPrescription", "IdMedicament" });
        }
    }
}
