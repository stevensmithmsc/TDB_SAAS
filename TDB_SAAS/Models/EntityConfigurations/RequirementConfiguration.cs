using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class RequirementConfiguration : EntityTypeConfiguration<Requirement>
    {
        public RequirementConfiguration()
        {
            ToTable("Req");

            HasRequired(r => r.Staff)
                .WithMany(p => p.Requirements)
                .HasForeignKey(r => r.StaffID);

            HasRequired(r => r.Course)
                .WithMany(c => c.RequiredBy)
                .HasForeignKey(r => r.CourseID);

            HasRequired(r => r.Status)
                .WithMany(s => s.RequirementsWithStatus)
                .HasForeignKey(r => r.StatusID);

            Property(r => r.Comments)
                .HasColumnType("text");

            HasRequired(r => r.Creator)
                .WithMany(p => p.RequirementsCreated)
                .HasForeignKey(r => r.CreatorID);

            HasRequired(r => r.Modifier)
                .WithMany(p => p.RequirementsModified)
                .HasForeignKey(r => r.ModifierID);
        }
    }
}