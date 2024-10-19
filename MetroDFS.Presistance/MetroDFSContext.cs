using MetroDFS.Models.Models;
using MetroDFS.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MetroDFS.Presistance
{
    public class MetroDFSContext :DbContext
    {
        public MetroDFSContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<ChildStation> ChildrensStations { get; set; }
    }
}
