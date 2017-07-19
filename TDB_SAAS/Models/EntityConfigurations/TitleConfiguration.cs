using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class TitleConfiguration : EntityTypeConfiguration<Title>
    {
        public TitleConfiguration()
        {
            Property(t => t.TitleValue)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnType("varchar")
                .HasColumnName("Title");

            Property(t => t.AvailGenders)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnType("varchar")
                .HasColumnName("Genders");

            HasRequired(t => t.Creator)
                .WithMany(p => p.TitlesCreated)
                .HasForeignKey(t => t.CreatorID);

            HasRequired(t => t.Modifier)
                .WithMany(p => p.TitlesModified)
                .HasForeignKey(t => t.ModifierID);
        }

    }
}