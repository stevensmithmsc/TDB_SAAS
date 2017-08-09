using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            Property(t => t.TeamName)
               .HasMaxLength(50)
               .HasColumnType("varchar")
               .HasColumnName("Team");

            HasOptional(t => t.Leader)
                .WithMany(p => p.LeaderOf)
                .HasForeignKey(t => t.LeaderID);

            HasRequired(t => t.Creator)
                .WithMany(p => p.TeamsCreated)
                .HasForeignKey(t => t.CreatorID);

            HasRequired(t => t.Modifier)
                .WithMany(p => p.TeamsModified)
                .HasForeignKey(t => t.ModifierID);
        }
    }
}