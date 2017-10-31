using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class TNAConfiguration : EntityTypeConfiguration<TNA>
    {
        public TNAConfiguration()
        {
            HasKey(t => t.StaffID);

            HasRequired(t => t.Staff)
                .WithOptional(p => p.TNA);

            HasOptional(t => t.Trainer)
                .WithMany(p => p.TNAsAsTrainer)
                .HasForeignKey(t => t.TrainerID);

            Property(t => t.ContactOutcome)
                .HasColumnType("text");

            HasOptional(t => t.Outcome)
                .WithMany(s => s.TNAsWithStatus)
                .HasForeignKey(t => t.OutcomeID);

            HasRequired(t => t.Creator)
                .WithMany(p => p.TNAsCreated)
                .HasForeignKey(t => t.CreatorID);

            HasRequired(t => t.Modifier)
                .WithMany(p => p.TNAsModified)
                .HasForeignKey(t => t.ModifierID);
        }
    }
}