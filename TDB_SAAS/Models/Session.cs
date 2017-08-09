using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Session
    {
        public int ID { get; set; }

        public Nullable<int> CourseID { get; set; }
        public virtual Course Course { get; set; }

        public Nullable<int> TrainerID { get; set; }
        public virtual Person Trainer { get; set; }

        public Nullable<short> LocationID { get; set; }
        public virtual Location Location { get; set; }

        public Nullable<System.DateTime> Strt { get; set; }

        public Nullable<System.DateTime> Endt { get; set; }

        public short MaxP { get; set; }

        public string Notes { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        //public virtual DateTime? StartDate
        //{
        //    get { if (Strt == null) return null; else return ((DateTime)Strt).Date; }
        //    set { if (value != null) { if (Strt == null) Strt = value; else { Strt = ((DateTime)value).Date.Add(((DateTime)Strt).TimeOfDay); } } }
        //}
        //public virtual TimeSpan? StartTime
        //{
        //    get { if (Strt == null) return null; else return ((DateTime)Strt).TimeOfDay; }
        //    set { if (value != null) { if (Strt == null) Strt = DateTime.Now.Date.Add((TimeSpan)value); else { Strt = ((DateTime)Strt).Date.Add((TimeSpan)value); } } }
        //}

        //public virtual TimeSpan? EndTime
        //{
        //    get { if (Endt == null) return null; else return ((DateTime)Endt).TimeOfDay; }
        //    set { if (value != null) { if (Endt == null && Strt == null) Endt = DateTime.Now.Date.Add((TimeSpan)value); else if (Endt == null) { Endt = ((DateTime)Strt).Date.Add((TimeSpan)value); } else { Endt = ((DateTime)Endt).Date.Add((TimeSpan)value); } } }
        //}

        public virtual int Bookings { get { return Attendances.Where(a => a.OutcomeID != 6 && a.OutcomeID != 7).Count(); } }
        public virtual int UnoutcommedBookings { get { return Attendances.Where(a => a.OutcomeID == 0).Count(); } }
        public virtual int AvailablePlaces { get { return (MaxP - Bookings); } }

        public Session()
        {
            Attendances = new HashSet<Attendance>();
        }
    }
}