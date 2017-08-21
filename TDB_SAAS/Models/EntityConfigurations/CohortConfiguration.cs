using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class CohortConfiguration : EntityTypeConfiguration<Cohort>
    {
        public CohortConfiguration()
        {
            Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("varchar");

            Property(c => c.CohortDescription)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnType("varchar")
                .HasColumnName("Cohort");

            Property(c => c.Number)
                .HasPrecision(3, 1);

            Property(c => c.Notes)
                .HasColumnType("text");

            HasRequired(c => c.Creator)
                .WithMany(p => p.CohortsCreated)
                .HasForeignKey(c => c.CreatorID);

            HasRequired(c => c.Modifier)
                .WithMany(p => p.CohortsModified)
                .HasForeignKey(c => c.ModifierID);
        }
    }
}