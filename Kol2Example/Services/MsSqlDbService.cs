using Kol2Example.DTO_s.Requests;
using Kol2Example.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kol2Example.Services
{
    public class MsSqlDbService : IDbService
    {
        public CodeFirstContext dbContext;

        public MsSqlDbService(CodeFirstContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool addOrder(int index, ClientDataRequst request)
        {
            try
            {
                if (dbContext.Klient.SingleOrDefault(c => c.IdKlient == index) == null)
                    return false;
                List<int> productsId = new List<int>();
                ICollection<ZamowienieWyrobCukierniczy> zw = new List<ZamowienieWyrobCukierniczy>();
                foreach (ClientProduct product in request.Wyroby)
                {
                    WyrobCukierniczy tmpProduct = dbContext.WyrobCukierniczy.SingleOrDefault(wc => wc.Nazwa == product.Wyrob);
                    if (tmpProduct == null)
                        return false;
                    zw.Add(
                       new ZamowienieWyrobCukierniczy
                       {
                           IdWyrobuCukierniczego = tmpProduct.IdWyrobuCukierniczego
                                                                      ,
                           Illosc = product.Ilosc
                                                                      ,
                           Uwagi = request.Uwagi
                       });
                }
                var pracowniki = dbContext.Pracownik.Select(p => p.IdPracownik).ToList();
                var random = new Random();
                var id = random.Next(pracowniki.Count());

                Zamowienie newOrder = new Zamowienie { DataPrzyjencia = request.DataPrzyjencia, IdKlient = index, Uwagi = request.Uwagi, IdPracownik = id, ZamowieniaWyrobyCukierniczie = zw };
                dbContext.Zamowienie.Add(newOrder);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) { 
            return false;
            }
        }

        public IEnumerable<Zamowienie> getOrdersByClientSurname(string Surname)
        {
            try
            {
                if (Surname == null)
                {
                    return dbContext.Zamowienie
                                     .Include(z => z.ZamowieniaWyrobyCukierniczie)
                                     .ThenInclude(zw => zw.WyrobCukierniczy)
                                     .Select(z => new Zamowienie
                                     {
                                         DataPrzyjencia = z.DataPrzyjencia
                                         ,
                                         DataRealizacji = z.DataRealizacji
                                         ,
                                         IdZamowienia = z.IdZamowienia
                                         ,
                                         ZamowieniaWyrobyCukierniczie = z.ZamowieniaWyrobyCukierniczie.Select(zw => new ZamowienieWyrobCukierniczy
                                         {
                                             IdWyrobuCukierniczego = zw.IdWyrobuCukierniczego
                                                                                                             ,
                                             Uwagi = zw.Uwagi
                                                                                                             ,
                                             Illosc = zw.Illosc
                                                                                                             ,
                                             WyrobCukierniczy = new WyrobCukierniczy
                                             {
                                                 IdWyrobuCukierniczego = zw.IdWyrobuCukierniczego
                                                                                                                 ,
                                                 CenaZaSzt = zw.WyrobCukierniczy.CenaZaSzt
                                                                                                                 ,
                                                 Typ = zw.WyrobCukierniczy.Typ
                                                                                                                 ,
                                                 Nazwa = zw.WyrobCukierniczy.Nazwa
                                             }




                                         })
                                     })
                                     .ToList();
                }

                var Client = dbContext.Klient.Where(k => k.Nazwisko == Surname).SingleOrDefault();
                if (Client == null)
                {
                    return null;
                }
                return dbContext.Zamowienie.Where(z => z.IdKlient == Client.IdKlient)
                                               .Include(z => z.ZamowieniaWyrobyCukierniczie)
                                               .ThenInclude(zw => zw.WyrobCukierniczy)
                                               .Select(z => new Zamowienie
                                               {
                                                   DataPrzyjencia = z.DataPrzyjencia
                                                                             ,
                                                   DataRealizacji = z.DataRealizacji
                                                                             ,
                                                   IdZamowienia = z.IdZamowienia
                                                                             ,
                                                   ZamowieniaWyrobyCukierniczie = z.ZamowieniaWyrobyCukierniczie
                                                                                                              .Select(zw => new ZamowienieWyrobCukierniczy
                                                                                                              {
                                                                                                                  IdWyrobuCukierniczego = zw.IdWyrobuCukierniczego
                                                                                                              ,
                                                                                                                  Uwagi = zw.Uwagi
                                                                                                              ,
                                                                                                                  Illosc = zw.Illosc
                                                                                                             ,
                                                                                                                  WyrobCukierniczy = new WyrobCukierniczy
                                                                                                                  {
                                                                                                                      IdWyrobuCukierniczego = zw.IdWyrobuCukierniczego
                                                                                                                                   ,
                                                                                                                      CenaZaSzt = zw.WyrobCukierniczy.CenaZaSzt
                                                                                                                                   ,
                                                                                                                      Typ = zw.WyrobCukierniczy.Typ
                                                                                                                                   ,
                                                                                                                      Nazwa = zw.WyrobCukierniczy.Nazwa
                                                                                                                  }




                                                                                                              })
                                               })
                                               .ToList();
            }
            catch (Exception ex) {
                return null;
            }
        }
    }
}
