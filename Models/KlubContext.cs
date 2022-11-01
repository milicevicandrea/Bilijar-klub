using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class KlubContext: DbContext
    {
        public DbSet<Sto> Stolovi { get; set; }
    }
}