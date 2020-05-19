using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cw10.Migrations
{
    public partial class DataFilled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "example1@gmail.com", "Jacob", "Miller" },
                    { 2, "example2@gmail.com", "Sarah", "Johnson" },
                    { 3, "example3@gmail.com", "Bill", "Brown" }
                });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Prescribed for: Thyroid deficiency", "Synthroid", "Thyroxines" },
                    { 2, "Prescribed for: Bacterial infections", "Amoxilicine", "Antibiotics" },
                    { 3, "Prescribed for: Seizures, nerve pain", "Neurontin", "Anti-epileptics" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "Birthdate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local), "Alex", "Johnson" },
                    { 2, new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local), "Sarah", "Johnson" },
                    { 3, new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local), "Bill", "Brown" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 1, new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 2, new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), 2, 2 });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 3, new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), 3, 3 });

            migrationBuilder.InsertData(
                table: "Prescription_Mdedicament",
                columns: new[] { "IdPrescription", "IdMedicament", "Details", "Dose" },
                values: new object[] { 1, 1, "some details1", 10 });

            migrationBuilder.InsertData(
                table: "Prescription_Mdedicament",
                columns: new[] { "IdPrescription", "IdMedicament", "Details", "Dose" },
                values: new object[] { 2, 2, "some details2", 5 });

            migrationBuilder.InsertData(
                table: "Prescription_Mdedicament",
                columns: new[] { "IdPrescription", "IdMedicament", "Details", "Dose" },
                values: new object[] { 3, 3, "some details3", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prescription_Mdedicament",
                keyColumns: new[] { "IdPrescription", "IdMedicament" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Prescription_Mdedicament",
                keyColumns: new[] { "IdPrescription", "IdMedicament" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Prescription_Mdedicament",
                keyColumns: new[] { "IdPrescription", "IdMedicament" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 3);
        }
    }
}
