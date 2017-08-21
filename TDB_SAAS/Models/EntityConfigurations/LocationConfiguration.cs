using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration()
        {
            Property(l => l.LocationName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar")
                .HasColumnName("Location");

            HasRequired(l => l.Creator)
                .WithMany(p => p.LocationsCreated)
                .HasForeignKey(l => l.CreatorID);

            HasRequired(l => l.Modifier)
                .WithMany(p => p.LocationsModified)
                .HasForeignKey(l => l.ModifierID);
        }
    }
}