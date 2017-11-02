using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class RAConfiguration : EntityTypeConfiguration<RA>
    {
        public RAConfiguration()
        {
            HasKey(r => r.StaffID);

            HasRequired(r => r.Staff)
                .WithOptional(p => p.RA);

            Property(r => r.UUID)
                .HasColumnType("varchar")
                .HasMaxLength(12);

            HasOptional(r => r.PDSRole)
                .WithMany(s => s.RAsWithPDSRole)
                .HasForeignKey(r => r.PDSRoleID);

            HasOptional(r => r.PlusUpdated)
                .WithMany(s => s.RAsWithPlusUpdated)
                .HasForeignKey(r => r.PLUSUpdatedID);

            HasOptional(r => r.ESRUpdated)
                .WithMany(s => s.RAsWithESRUpdated)
                .HasForeignKey(r => r.ESRUpdatedID);

            Property(r => r.RAComments)
                .HasColumnType("text");

            HasRequired(r => r.Creator)
                .WithMany(p => p.RAsCreated)
                .HasForeignKey(r => r.CreatorID);

            HasRequired(r => r.Modifier)
                .WithMany(p => p.RAsModified)
                .HasForeignKey(r => r.ModifierID);
        }
    }
}