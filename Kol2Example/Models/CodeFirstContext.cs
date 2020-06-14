using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kol2Example.Models
{
    public class CodeFirstContext:DbContext
    {
        public DbSet<Pracownik> Pracownik{ get; set; }
        public DbSet<Klient> Klient { get; set; }
        public DbSet<Zamowienie> Zamowienie { get; set; }
        public DbSet<WyrobCukierniczy> WyrobCukierniczy { get; set; }
        public DbSet<ZamowienieWyrobCukierniczy> ZamowienieWyrobCukierniczy { get; set; }
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Pracownik>(entity =>
            {
                entity.HasKey(p => p.IdPracownik);

                entity.Property(p => p.Imie).HasMaxLength(50).IsRequired();

                entity.Property(p => p.Nazwisko).HasMaxLength(60).IsRequired();

                var fillList = new List<Pracownik>();

                fillList.Add(new Pracownik { IdPracownik = 1, Imie = "John", Nazwisko = "Johnson" });

                fillList.Add(new Pracownik { IdPracownik = 2, Imie = "Alice", Nazwisko = "Johnson" });

                fillList.Add(new Pracownik { IdPracownik = 3, Imie = "Sarash", Nazwisko = "Stephenson" });

                entity.HasData(fillList);

            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(p => p.IdKlient);

                entity.Property(p => p.Imie).HasMaxLength(50).IsRequired();

                entity.Property(p => p.Nazwisko).HasMaxLength(60).IsRequired();

                var fillList = new List<Klient>();

                fillList.Add(new Klient { IdKlient = 1, Imie = "Bill", Nazwisko = "Wazowski" });

                fillList.Add(new Klient { IdKlient = 2, Imie = "Alicja", Nazwisko = "Piotrowska" });

                fillList.Add(new Klient { IdKlient = 3, Imie = "Bob", Nazwisko = "Kowelski" });

                modelBuilder.Entity<Klient>().HasData(fillList);

            });

            modelBuilder.Entity<Zamowienie>(entity =>
            {
                entity.HasKey(z => z.IdZamowienia);

                entity.Property(z => z.Uwagi).HasMaxLength(300);

                entity.Property(z => z.DataPrzyjencia).IsRequired();

                entity.HasOne(z => z.Pracownik)
                                                 .WithMany(p => p.Zamowienia)
                                                 .HasForeignKey(z => z.IdPracownik)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("Zamowienie_Pracownik");

                entity.HasOne(z => z.Klient)
                                                .WithMany(p => p.Zamowienia)
                                                .HasForeignKey(z => z.IdKlient)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("Zamowienie_Klient");

                var fillList = new List<Zamowienie>();

                fillList.Add(new Zamowienie { IdZamowienia = 1, Uwagi = "ababababba", DataPrzyjencia = DateTime.Now, DataRealizacji = DateTime.Now.AddDays(30), IdKlient = 1, IdPracownik = 1 });

                fillList.Add(new Zamowienie { IdZamowienia = 2, Uwagi = "hello2", DataPrzyjencia = DateTime.Now, DataRealizacji = DateTime.Now.AddDays(50), IdKlient = 2, IdPracownik = 2 });

                fillList.Add(new Zamowienie { IdZamowienia = 3, Uwagi = "example3", DataPrzyjencia = DateTime.Now, DataRealizacji = DateTime.Now.AddDays(40), IdKlient = 3, IdPracownik = 2 });

                modelBuilder.Entity<Zamowienie>().HasData(fillList);

            });

            modelBuilder.Entity<WyrobCukierniczy>(entity =>
            {
                entity.HasKey(w => w.IdWyrobuCukierniczego);

                entity.Property(w => w.Nazwa).HasMaxLength(200).IsRequired();

                entity.Property(w => w.Typ).HasMaxLength(40).IsRequired();

                var fillList = new List<WyrobCukierniczy>();

                fillList.Add(new WyrobCukierniczy { IdWyrobuCukierniczego = 1, Nazwa = "example1", Typ = "Typ1", CenaZaSzt = 30 });

                fillList.Add(new WyrobCukierniczy { IdWyrobuCukierniczego = 2, Nazwa = "example2", Typ = "Typ2", CenaZaSzt = 10 });

                fillList.Add(new WyrobCukierniczy { IdWyrobuCukierniczego = 3, Nazwa = "example3", Typ = "Typ3", CenaZaSzt = 20 });

                modelBuilder.Entity<WyrobCukierniczy>().HasData(fillList);

            });

            modelBuilder.Entity<ZamowienieWyrobCukierniczy>(entity =>
            {
                entity.ToTable("Zamowienie_WyrobCukierniczy");

                entity.HasKey(zw => new { zw.IdWyrobuCukierniczego, zw.IdZamowienia });

                entity.Property(zw => zw.Uwagi).HasMaxLength(300);

                entity.HasOne(z => z.Zamowienie)
                                                .WithMany(p => p.ZamowieniaWyrobyCukierniczie)
                                                .HasForeignKey(z => z.IdZamowienia)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("ZamowienieWyrobCukierniczy_Zamowienie");

                entity.HasOne(z => z.WyrobCukierniczy)
                                                .WithMany(p => p.ZamowieniaWyrobyCukierniczie)
                                                .HasForeignKey(z => z.IdWyrobuCukierniczego)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("ZamowienieWyrobCukierniczy_WyrobCukierniczy");

                var fillList = new List<ZamowienieWyrobCukierniczy>();

                fillList.Add(new ZamowienieWyrobCukierniczy { IdWyrobuCukierniczego = 1, IdZamowienia = 1, Illosc = 5, Uwagi = "exampleUwagi1" });

                fillList.Add(new ZamowienieWyrobCukierniczy { IdWyrobuCukierniczego = 2, IdZamowienia = 2, Illosc = 1, Uwagi = "exampleUwagi2" });

                fillList.Add(new ZamowienieWyrobCukierniczy { IdWyrobuCukierniczego = 2, IdZamowienia = 3, Illosc = 7, Uwagi = "exampleUwagi3" });

                modelBuilder.Entity<ZamowienieWyrobCukierniczy>().HasData(fillList);

            });
        }
    }
}
