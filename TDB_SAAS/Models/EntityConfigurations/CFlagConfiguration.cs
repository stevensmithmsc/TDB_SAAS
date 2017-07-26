using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class CFlagConfiguration : EntityTypeConfiguration<CFlag>
    {
        public CFlagConfiguration()
        {
            Property(f => f.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(f => f.Code)
                .HasMaxLength(3)
                .HasColumnType("char")
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));

            Property(f => f.TheFlag)
                .HasMaxLength(50)
                .HasColumnType("varchar")
                .HasColumnName("Description");

            HasRequired(f => f.Creator)
                .WithMany(p => p.CFlagsCreated)
                .HasForeignKey(f => f.CreatorID);

            HasRequired(f => f.Modifier)
                .WithMany(p => p.CFlagsModified)
                .HasForeignKey(f => f.ModifierID);
        }
    }
}