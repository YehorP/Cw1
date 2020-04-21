using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public interface IPasswordService
    {
        String HashPassword(String password, String salt);
        String CreateSalt();
        bool ValidatePassword(String hash, String password, String salt);
    }
}
