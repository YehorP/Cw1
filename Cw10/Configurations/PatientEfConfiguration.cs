using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Configurations
{
    public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
          
            builder.HasKey(e => e.IdPatient);

            builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();

            builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();

            builder.Property(e => e.Birthdate).IsRequired();

            var patients = new List<Patient>();

            patients.Add(new Patient { Birthdate = DateTime.Today, FirstName = "Alex", LastName = "Johnson", IdPatient = 1 });

            patients.Add(new Patient { Birthdate = DateTime.Today, FirstName = "Sarah", LastName = "Johnson", IdPatient = 2 });

            patients.Add(new Patient { Birthdate = DateTime.Today, FirstName = "Bill", LastName = "Brown", IdPatient = 3 });

            builder.HasData(patients);
        }
    }
}
