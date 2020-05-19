using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Configurations
{
    public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(e => e.IdDoctor);

            builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();

            builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();

            builder.Property(e => e.Email).HasMaxLength(100).IsRequired();


            var doctors = new List<Doctor>();

            doctors.Add(new Doctor { IdDoctor = 1, FirstName = "Jacob", LastName = "Miller", Email ="example1@gmail.com"  });
             
            doctors.Add(new Doctor { IdDoctor = 2, FirstName = "Sarah", LastName = "Johnson", Email = "example2@gmail.com" });

            doctors.Add(new Doctor { IdDoctor = 3, FirstName = "Bill", LastName = "Brown", Email = "example3@gmail.com" });

            builder.HasData(doctors);
        }
    }
}
