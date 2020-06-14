using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kol2Example.Models
{
    public class ZamowienieWyrobCukierniczy
    {
        public int IdWyrobuCukierniczego { get; set; }
        public int IdZamowienia { get; set; }
        public int Illosc { get; set; }
        public string Uwagi { get; set; }
        public virtual Zamowienie Zamowienie { get; set; }
        public virtual WyrobCukierniczy WyrobCukierniczy { get; set; }
    }
}
