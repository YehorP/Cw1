using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kol2Example.Models
{
    public class Zamowienie
    {
        public int IdZamowienia { get; set; }
        public DateTime DataPrzyjencia { get; set; }
        public DateTime? DataRealizacji { get; set; }
        public string Uwagi { get; set; }
        public  int IdPracownik { get; set; }
        public int IdKlient { get; set; }
        public virtual Pracownik Pracownik { get; set; }
        public virtual Klient Klient { get; set; }
        public virtual IEnumerable<ZamowienieWyrobCukierniczy> ZamowieniaWyrobyCukierniczie { get; set; }
    }
}
