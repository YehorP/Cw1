using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Configurations
{
    public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<Models.PrescriptionMedicament>
    {
        public void Configure(EntityTypeBuilder<Models.PrescriptionMedicament> builder)
        {
            builder.ToTable("Prescription_Medicament");

            builder.HasKey(e => new { e.IdPrescription, e.IdMedicament });

            builder.Property(e => e.Details).HasMaxLength(100).IsRequired();

            builder.Property(e => e.Dose).IsRequired(false);

            builder.HasOne(e => e.Prescription)
                  .WithMany(p => p.PrescriptionMedicaments)
                  .HasForeignKey(d => d.IdPrescription)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("PrescriptionMedicament_Prescriotion");

            builder.HasOne(e => e.Medicament)
                 .WithMany(p => p.PrescriptionMedicaments)
                 .HasForeignKey(d => d.IdMedicament)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("PrescriptionMedicament_Medicament");

            var PrescriptionsMedicaments = new List<PrescriptionMedicament>();

            PrescriptionsMedicaments.Add(new PrescriptionMedicament {IdPrescription = 1, IdMedicament = 1, Details = "some details1", Dose = 10});

            PrescriptionsMedicaments.Add(new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 2, Details = "some details2", Dose = 5 });

            PrescriptionsMedicaments.Add(new PrescriptionMedicament { IdPrescription = 3, IdMedicament = 3, Details = "some details3", Dose = 1 });

            builder.HasData(PrescriptionsMedicaments);
        }
    }
}
