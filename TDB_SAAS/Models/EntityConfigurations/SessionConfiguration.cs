using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class SessionConfiguration : EntityTypeConfiguration<Session>
    {
        public SessionConfiguration()
        {
            ToTable("Sess");

            HasOptional(s => s.Course)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CourseID);

            HasOptional(s => s.Trainer)
                .WithMany(p => p.SessionsTrained)
                .HasForeignKey(s => s.TrainerID);

            HasOptional(s => s.Location)
                .WithMany(l => l.Sessions)
                .HasForeignKey(s => s.LocationID);

            Property(s => s.Notes)
                .HasColumnType("text");

            HasRequired(s => s.Creator)
                .WithMany(p => p.SessionsCreated)
                .HasForeignKey(s => s.CreatorID);

            HasRequired(s => s.Modifier)
                .WithMany(p => p.SessionsModified)
                .HasForeignKey(s => s.ModifierID);
        }
    }
}