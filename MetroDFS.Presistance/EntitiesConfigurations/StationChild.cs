using MetroDFS.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroDFS.Presistance.EntitiesConfigurations
{
    internal class StationChild : IEntityTypeConfiguration<ChildStation>
    {
        public void Configure(EntityTypeBuilder<ChildStation> builder)
        {
            builder.HasKey(x => new { x.StationId, x.ParentStationId });

            builder.HasOne(x => x.ParentStation)
                .WithMany()
                .HasForeignKey(x => x.ParentStationId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
        }
    }
}
