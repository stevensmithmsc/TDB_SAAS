using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class TeamApprovalConfiguration : EntityTypeConfiguration<TeamApproval>
    {
        public TeamApprovalConfiguration()
        {
            ToTable("TeamApprov");

            HasRequired(t => t.Staff)
               .WithMany(p => p.TeamApprovals)
               .HasForeignKey(t => t.StaffID);

            Property(t => t.Team)
               .IsRequired()
               .HasMaxLength(100)
               .HasColumnType("varchar");

            Property(t => t.Details)
               .HasColumnType("text");

            HasRequired(t => t.Creator)
                .WithMany(p => p.TeamApprovalsCreated)
                .HasForeignKey(t => t.CreatorID);

            HasRequired(t => t.Modifier)
                .WithMany(p => p.TeamApprovalsModified)
                .HasForeignKey(t => t.ModifierID);
        }
    }
}