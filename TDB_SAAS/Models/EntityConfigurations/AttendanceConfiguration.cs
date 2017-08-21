using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasRequired(a => a.Staff)
                .WithMany(p => p.Attendances)
                .HasForeignKey(a => a.StaffID);

            HasRequired(a => a.Sess)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.SessID);

            HasRequired(a => a.Outcome)
                .WithMany(s => s.AttendanceWithStatus)
                .HasForeignKey(a => a.OutcomeID);

            Property(a => a.Comments)
                .HasColumnType("text");

            HasRequired(a => a.Booker)
                .WithMany(p => p.AttendancesBooked)
                .HasForeignKey(a => a.BookedBy);

            HasOptional(a => a.Canceller)
                .WithMany(p => p.AttendancesCancelled)
                .HasForeignKey(a => a.CancelledBy);

            HasRequired(a => a.Modifier)
                .WithMany(p => p.AttendancesModified)
                .HasForeignKey(a => a.ModifierID);

        }
    }
}