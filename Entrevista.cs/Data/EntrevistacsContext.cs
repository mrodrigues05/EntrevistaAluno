using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entrevista.Models;

namespace Entrevista.cs.Data
{
    public class EntrevistacsContext : DbContext
    {
        public EntrevistacsContext (DbContextOptions<EntrevistacsContext> options)
            : base(options)
        {
        }

        public DbSet<Entrevista.Models.Student> Student { get; set; } = default!;

        public DbSet<Entrevista.Models.Premium> Premium { get; set; } = default!;
    }
}
