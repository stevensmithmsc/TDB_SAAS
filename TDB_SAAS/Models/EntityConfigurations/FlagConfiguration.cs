using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class FlagConfiguration : EntityTypeConfiguration<Flag>
    {
        public FlagConfiguration()
        {
            Property(f => f.ID)
                .HasMaxLength(3)
                .HasColumnType("char");

            Property(f => f.TheFlag)
                .HasMaxLength(50)
                .HasColumnType("varchar")
                .HasColumnName("Description");

            HasRequired(f => f.Creator)
                .WithMany(p => p.FlagsCreated)
                .HasForeignKey(f => f.CreatorID);

            HasRequired(f => f.Modifier)
                .WithMany(p => p.FlagsModified)
                .HasForeignKey(f => f.ModifierID);
        }
    }
}