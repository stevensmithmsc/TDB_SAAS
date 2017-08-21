using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class BoroughConfiguration : EntityTypeConfiguration<Borough>
    {
        public BoroughConfiguration()
        {
            Property(b => b.ID)
                .HasMaxLength(1)
                .HasColumnType("char");

            Property(b => b.BoroughName)
                .HasMaxLength(15)
                .IsRequired()
                .HasColumnType("varchar")
                .HasColumnName("Borough");

            Property(b => b.LongName)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnType("varchar")
                .HasColumnName("Long");

            Property(b => b.ShortName)
                .HasMaxLength(3)
                .IsFixedLength()
                .IsRequired()
                .HasColumnType("varchar")
                .HasColumnName("Short");

            HasRequired(b => b.Creator)
                .WithMany(p => p.BoroughsCreated)
                .HasForeignKey(b => b.CreatorID);

            HasRequired(b => b.Modifier)
                .WithMany(p => p.BoroughsModified)
                .HasForeignKey(b => b.ModifierID);

        }
    }
}