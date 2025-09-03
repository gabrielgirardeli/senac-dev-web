using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext (
            DbContextOptions<MeuDbContext> opcoes) :base(opcoes)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

        }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
