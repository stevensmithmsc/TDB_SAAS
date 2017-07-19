using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            Property(c => c.CourseName)
               .HasMaxLength(80)
               .HasColumnType("varchar")
               .HasColumnName("Course");

            Property(c => c.Notes)
                .HasColumnType("text");

            HasMany(c => c.PreReqs)
                .WithMany(c => c.ReqFor)
                .Map(m =>
                {
                    m.ToTable("CoursePreReqs");
                    m.MapLeftKey("CourseID");
                    m.MapRightKey("PreReqID");
                });

            HasMany(c => c.Flags)
                .WithMany(f => f.Courses)
                .Map(m =>
                {
                    m.ToTable("CourseFlags");
                    m.MapLeftKey("CourseID");
                    m.MapRightKey("Flag");
                });

            HasRequired(c => c.Creator)
                .WithMany(p => p.CoursesCreated)
                .HasForeignKey(c => c.CreatorID);

            HasRequired(c => c.Modifier)
                .WithMany(p => p.CoursesModified)
                .HasForeignKey(c => c.ModifierID);

        }
    }
}