using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class SecurityDAO
    {
        internal int FindByUser(UserModel user)
        {
            if ((user.Korisnik == "Admin") && (user.Lozinka == "admin"))
            {
                return 1;
            }
            else
                if ((user.Korisnik == "Korisnik") && (user.Lozinka == "korisnik"))
                {
                return 2;
                }
            else
                return 3;
        }
    }
}