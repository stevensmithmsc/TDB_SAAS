using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            ToTable("Staff");

            HasOptional(p => p.Title)
                .WithMany(t => t.People)
                .HasForeignKey(p => p.TitleID);
            
            Property(p => p.Forename)
                .HasMaxLength(35)
                .HasColumnType("varchar")
                .HasColumnName("FName");

            Property(p => p.Surname)
                .IsRequired()
                .HasMaxLength(35)
                .HasColumnType("varchar")
                .HasColumnName("SName");

            Property(p => p.PreferredName)
                .HasMaxLength(70)
                .HasColumnType("varchar")
                .HasColumnName("PName");

            Property(p => p.JobTitle)
                .HasMaxLength(100)
                .HasColumnType("varchar");

            HasOptional(p => p.LineManager)
                .WithMany(p => p.Minions)
                .HasForeignKey(p => p.LineManID);

            Property(p => p.Phone)
                .HasMaxLength(20)
                .HasColumnType("varchar");

            Property(p => p.Email)
                .HasMaxLength(80)
                .HasColumnType("varchar");

            Property(p => p.MiddleName)
                .HasMaxLength(35)
                .HasColumnType("varchar")
                .HasColumnName("MName");

            Property(p => p.Comments)
                .HasColumnType("text");

            HasRequired(p => p.Creator)
                .WithMany(p => p.PeopleCreated)
                .HasForeignKey(p => p.CreatorID);

            HasRequired(p => p.Modifier)
                .WithMany(p => p.PeopleModified)
                .HasForeignKey(p => p.ModifierID);

            HasMany(p => p.Flags)
                .WithMany(f => f.People)
                .Map(m =>
               {
                   m.ToTable("StaffFlags");
                   m.MapLeftKey("StaffID");
                   m.MapRightKey("Flag");
               });

            HasMany(p => p.Boroughs)
                .WithMany(b => b.Staff)
                .Map(m =>
                {
                    m.ToTable("StaffBoroughs");
                    m.MapLeftKey("StaffID");
                    m.MapRightKey("BoroughID");
                });

        }

    }
}