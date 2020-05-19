using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Configurations
{
    public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(e => e.IdPrescription);

            builder.Property(e => e.Date).IsRequired();

            builder.Property(e => e.DueDate).IsRequired();

            builder.HasOne(e => e.Patient)
                  .WithMany(p => p.Prescriptions)
                  .HasForeignKey(d => d.IdPatient)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("Prescription_Patient");

            builder.HasOne(e => e.Doctor)
                      .WithMany(p => p.Prescriptions)
                      .HasForeignKey(d => d.IdDoctor)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("Prescription_Doctor");

            var prescriptions = new List<Prescription>();

            prescriptions.Add(new Prescription { IdPrescription = 1, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(30), IdDoctor = 1, IdPatient = 1 });

            prescriptions.Add(new Prescription { IdPrescription = 2, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(30), IdDoctor = 2, IdPatient = 2 });

            prescriptions.Add(new Prescription { IdPrescription = 3, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(30), IdDoctor = 3, IdPatient = 3 });

            builder.HasData(prescriptions);
        }
    }
}
