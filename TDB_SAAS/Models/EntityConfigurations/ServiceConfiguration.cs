using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class ServiceConfiguration : EntityTypeConfiguration<Service>
    {
        public ServiceConfiguration()
        {
            Property(s => s.ServiceName)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnType("varchar")
                .HasColumnName("Service");

            HasOptional(s => s.Parent)
                .WithMany(s => s.ChildServices)
                .HasForeignKey(s => s.ParentID);

            HasRequired(s => s.Creator)
                .WithMany(p => p.ServicesCreated)
                .HasForeignKey(s => s.CreatorID);

            HasRequired(s => s.Modifier)
                .WithMany(p => p.ServicesModified)
                .HasForeignKey(s => s.ModifierID);
        }
    }
}