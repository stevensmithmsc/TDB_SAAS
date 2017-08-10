using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class CostCentreConfiguration : EntityTypeConfiguration<CostCentre>
    {
        public CostCentreConfiguration()
        {
            HasKey(c => c.Code);

            Property(c => c.Code)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(c => c.CCName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar");

            HasRequired(c => c.Creator)
                .WithMany(p => p.CostCentresCreated)
                .HasForeignKey(c => c.CreatorID);

            HasRequired(c => c.Modifier)
                .WithMany(p => p.CostCentresModified)
                .HasForeignKey(c => c.ModifierID);
        }
    }
}