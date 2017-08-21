using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class SubjectiveConfiguration : EntityTypeConfiguration<Subjective>
    {
        public SubjectiveConfiguration()
        {
            HasKey(s => s.Code);

            Property(s => s.Code)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(s => s.SubName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar");

            HasRequired(s => s.Creator)
                .WithMany(p => p.SubjectivesCreated)
                .HasForeignKey(s => s.CreatorID);

            HasRequired(s => s.Modifier)
                .WithMany(p => p.SubjectivesModified)
                .HasForeignKey(s => s.ModifierID);
        }
    }
}