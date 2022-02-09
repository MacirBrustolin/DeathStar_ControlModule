using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStar_ControlModule.Data
{
    public class DeathStarContext : DbContext
    {
        public DeathStarContext(DbContextOptions<DeathStarContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoricoViagem>().HasOne(c => c.Naves).WithMany(t => t.HistoricoViagens);
            modelBuilder.Entity<HistoricoViagem>().HasOne(c => c.Pilotos).WithMany(t => t.HistoricoViagens);


        }

        public DbSet<Nave> Naves { get; set; }
        public DbSet<Piloto> Pilotos { get; set; }
        public DbSet<Planeta> Planetas { get; set; }
        public DbSet<HistoricoViagem> HistoricoViagens { get; set; }
    }
}
