using Kol2Example.DTO_s.Requests;
using Kol2Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kol2Example.Services
{
    public interface IDbService
    {
        public IEnumerable<Zamowienie> getOrdersByClientSurname(string Surname);
        public bool addOrder(int index, ClientDataRequst request);

        /*public int test();*/
    }
}
