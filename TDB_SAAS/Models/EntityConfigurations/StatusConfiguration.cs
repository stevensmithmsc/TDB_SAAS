using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class StatusConfiguration : EntityTypeConfiguration<Status>
    {
        public StatusConfiguration()
        {
            Property(s => s.StatusDesc)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnType("varchar")
                .HasColumnName("Status");

            HasRequired(s => s.Creator)
                .WithMany(p => p.StatusesCreated)
                .HasForeignKey(s => s.CreatorID);

            HasRequired(s => s.Modifier)
                .WithMany(p => p.StatusesModified)
                .HasForeignKey(s => s.ModifierID);

        }
    }
}