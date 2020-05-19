using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Configurations
{
    public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
           builder.HasKey(e => e.IdMedicament);

           builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

           builder.Property(e => e.Description).HasMaxLength(100).IsRequired();

           builder.Property(e => e.Type).HasMaxLength(100).IsRequired();


            var medicaments = new List<Medicament>();

            medicaments.Add(new Medicament {IdMedicament = 1, Name = "Synthroid", Description = "Prescribed for: Thyroid deficiency", Type = "Thyroxines" });

            medicaments.Add(new Medicament { IdMedicament = 2, Name = "Amoxilicine", Description = "Prescribed for: Bacterial infections", Type = "Antibiotics" });

            medicaments.Add(new Medicament { IdMedicament = 3, Name = "Neurontin", Description = "Prescribed for: Seizures, nerve pain", Type = "Anti-epileptics" });

            builder.HasData(medicaments);
        }
    }
}
