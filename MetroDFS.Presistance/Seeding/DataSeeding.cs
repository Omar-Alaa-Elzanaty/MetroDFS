using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetroDFS.Presistance.Seeding
{
    public class DataSeeding
    {
        public async static void Initialize(IServiceProvider service)
        {
            var dbcontext = service.GetRequiredService<MetroDFSContext>();
            var appliedMigrations = dbcontext.Database.GetAppliedMigrations();

            if (appliedMigrations.Count() == 0)
            {
                await dbcontext.Database.MigrateAsync();
            }
        }
    }
}
