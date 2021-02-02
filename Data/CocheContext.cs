using Microsoft.EntityFrameworkCore;
using PracticaExamen2Febrero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaExamen2Febrero.Data
{
    public class CocheContext : DbContext
    {
        public CocheContext(DbContextOptions<CocheContext> options) : base(options)
        { }

        public DbSet<Coche> Coches { get; set; }

    }
}
