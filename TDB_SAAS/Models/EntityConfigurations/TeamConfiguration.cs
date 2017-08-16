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

            HasMany(t => t.Boroughs)
                .WithMany(b => b.Teams)
                .Map(m =>
                {
                    m.ToTable("TeamBoroughs");
                    m.MapLeftKey("TeamID");
                    m.MapRightKey("BoroughID");
                });

            HasRequired(t => t.Creator)
                .WithMany(p => p.TeamsCreated)
                .HasForeignKey(t => t.CreatorID);

            HasRequired(t => t.Modifier)
                .WithMany(p => p.TeamsModified)
                .HasForeignKey(t => t.ModifierID);

            HasOptional(t => t.Finance)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.FinanceCode);

            HasMany(t => t.Services)
                .WithMany(s => s.Teams)
                .Map(m =>
                {
                    m.ToTable("TeamServices");
                    m.MapLeftKey("TeamID");
                    m.MapRightKey("ServiceID");
                });

            HasOptional(t => t.Cohort)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.CohortID);
        }
    }
}