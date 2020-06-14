using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kol2Example.DTO_s.Requests
{
    public class ClientDataRequst
    {
        [Required]
        public DateTime DataPrzyjencia { get; set; }
        [Required]
        [MaxLength(300)]
        public string Uwagi { get; set; }
        [Required]
        [MaxLength(300)]
        public IEnumerable<ClientProduct> Wyroby { get; set; }
    }
}
