﻿// <auto-generated />
using System;
using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cw10.Migrations
{
    [DbContext(typeof(CodeFirstContext))]
    [Migration("20200519141512_DataFilled")]
    partial class DataFilled
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cw10.Models.Doctor", b =>
                {
                    b.Property<int>("IdDoctor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IdDoctor");

                    b.ToTable("Doctor");

                    b.HasData(
                        new
                        {
                            IdDoctor = 1,
                            Email = "example1@gmail.com",
                            FirstName = "Jacob",
                            LastName = "Miller"
                        },
                        new
                        {
                            IdDoctor = 2,
                            Email = "example2@gmail.com",
                            FirstName = "Sarah",
                            LastName = "Johnson"
                        },
                        new
                        {
                            IdDoctor = 3,
                            Email = "example3@gmail.com",
                            FirstName = "Bill",
                            LastName = "Brown"
                        });
                });

            modelBuilder.Entity("Cw10.Models.Medicament", b =>
                {
                    b.Property<int>("IdMedicament")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IdMedicament");

                    b.ToTable("Medicament");

                    b.HasData(
                        new
                        {
                            IdMedicament = 1,
                            Description = "Prescribed for: Thyroid deficiency",
                            Name = "Synthroid",
                            Type = "Thyroxines"
                        },
                        new
                        {
                            IdMedicament = 2,
                            Description = "Prescribed for: Bacterial infections",
                            Name = "Amoxilicine",
                            Type = "Antibiotics"
                        },
                        new
                        {
                            IdMedicament = 3,
                            Description = "Prescribed for: Seizures, nerve pain",
                            Name = "Neurontin",
                            Type = "Anti-epileptics"
                        });
                });

            modelBuilder.Entity("Cw10.Models.Patient", b =>
                {
                    b.Property<int>("IdPatient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IdPatient");

                    b.ToTable("Patient");

                    b.HasData(
                        new
                        {
                            IdPatient = 1,
                            Birthdate = new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "Alex",
                            LastName = "Johnson"
                        },
                        new
                        {
                            IdPatient = 2,
                            Birthdate = new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "Sarah",
                            LastName = "Johnson"
                        },
                        new
                        {
                            IdPatient = 3,
                            Birthdate = new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "Bill",
                            LastName = "Brown"
                        });
                });

            modelBuilder.Entity("Cw10.Models.Prescription", b =>
                {
                    b.Property<int>("IdPrescription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdDoctor")
                        .HasColumnType("int");

                    b.Property<int>("IdPatient")
                        .HasColumnType("int");

                    b.HasKey("IdPrescription");

                    b.HasIndex("IdDoctor");

                    b.HasIndex("IdPatient");

                    b.ToTable("Prescription");

                    b.HasData(
                        new
                        {
                            IdPrescription = 1,
                            Date = new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            DueDate = new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Local),
                            IdDoctor = 1,
                            IdPatient = 1
                        },
                        new
                        {
                            IdPrescription = 2,
                            Date = new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            DueDate = new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Local),
                            IdDoctor = 2,
                            IdPatient = 2
                        },
                        new
                        {
                            IdPrescription = 3,
                            Date = new DateTime(2020, 5, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            DueDate = new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Local),
                            IdDoctor = 3,
                            IdPatient = 3
                        });
                });

            modelBuilder.Entity("Cw10.Models.PrescriptionMedicament", b =>
                {
                    b.Property<int>("IdPrescription")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicament")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Dose")
                        .HasColumnType("int");

                    b.HasKey("IdPrescription", "IdMedicament");

                    b.HasIndex("IdMedicament");

                    b.ToTable("Prescription_Mdedicament");

                    b.HasData(
                        new
                        {
                            IdPrescription = 1,
                            IdMedicament = 1,
                            Details = "some details1",
                            Dose = 10
                        },
                        new
                        {
                            IdPrescription = 2,
                            IdMedicament = 2,
                            Details = "some details2",
                            Dose = 5
                        },
                        new
                        {
                            IdPrescription = 3,
                            IdMedicament = 3,
                            Details = "some details3",
                            Dose = 1
                        });
                });

            modelBuilder.Entity("Cw10.Models.Prescription", b =>
                {
                    b.HasOne("Cw10.Models.Doctor", "Doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdDoctor")
                        .HasConstraintName("Prescription_Doctor")
                        .IsRequired();

                    b.HasOne("Cw10.Models.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdPatient")
                        .HasConstraintName("Prescription_Patient")
                        .IsRequired();
                });

            modelBuilder.Entity("Cw10.Models.PrescriptionMedicament", b =>
                {
                    b.HasOne("Cw10.Models.Medicament", "Medicament")
                        .WithMany("PrescriptionMedicaments")
                        .HasForeignKey("IdMedicament")
                        .HasConstraintName("PrescriptionMedicament_Medicament")
                        .IsRequired();

                    b.HasOne("Cw10.Models.Prescription", "Prescription")
                        .WithMany("PrescriptionMedicaments")
                        .HasForeignKey("IdPrescription")
                        .HasConstraintName("PrescriptionMedicament_Prescriotion")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
