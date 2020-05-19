using Cw10.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Models
{
    public class CodeFirstContext:DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            
            modelBuilder.ApplyConfiguration(new PatientEfConfiguration());

            modelBuilder.ApplyConfiguration(new DoctorEfConfiguration());

            modelBuilder.ApplyConfiguration(new MedicamentEfConfiguration());

            modelBuilder.ApplyConfiguration(new PrescriptionMedicamentEfConfiguration());
            
            modelBuilder.ApplyConfiguration(new PrescriptionEfConfiguration());
        }
    }
}
