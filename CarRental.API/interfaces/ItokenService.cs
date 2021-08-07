using CarRental.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.interfaces
{
    interface ItokenService
    {
        string CreateToken(User user);
    }
}
