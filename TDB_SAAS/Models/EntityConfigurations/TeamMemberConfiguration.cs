using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class TeamMemberConfiguration : EntityTypeConfiguration<TeamMember>
    {
        public TeamMemberConfiguration()
        {
            ToTable("TeamMem");

            HasRequired(t => t.Staff)
                .WithMany(p => p.MemberOf)
                .HasForeignKey(t => t.StaffID);

            HasRequired(t => t.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(t => t.TeamID);

            Property(t => t.Assignment)
               .HasMaxLength(12)
               .HasColumnType("varchar");

            HasRequired(t => t.Creator)
                .WithMany(p => p.TeamMembersCreated)
                .HasForeignKey(t => t.CreatorID);

            HasRequired(t => t.Modifier)
                .WithMany(p => p.TeamMembersModified)
                .HasForeignKey(t => t.ModifierID);
        }
    }
}