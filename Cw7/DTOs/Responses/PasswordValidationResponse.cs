using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.DTOs.Responses
{
    public class PasswordValidationResponse
    {
        public String Password { get; set; }
        public String Salt { get; set; }
    }
}
