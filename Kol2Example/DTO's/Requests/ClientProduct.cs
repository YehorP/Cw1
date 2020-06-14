using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kol2Example.DTO_s.Requests
{
    public class ClientProduct
    {
        [Required]
        [MaxLength(200)]
        public string Wyrob { get; set; }
        [Required]
        [MaxLength(300)]
        public string Uwagi { get; set; }
        [Required]
        public int Ilosc { get; set; }
    }
}
