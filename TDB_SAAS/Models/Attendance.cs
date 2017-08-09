using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Attendance
    {
        public int ID { get; set; }

        public int StaffID { get; set; }
        public virtual Person Staff { get; set; }

        public int SessID { get; set; }
        public virtual Session Sess { get; set; }

        public short OutcomeID { get; set; }
        public virtual Status Outcome { get; set; }

        public string Comments { get; set; }

        public int BookedBy { get; set; }
        public virtual Person Booker { get; set; }

        public DateTime Booked { get; set; }

        public Nullable<System.DateTime> Cancelled { get; set; }

        public Nullable<int> CancelledBy { get; set; }
        public virtual Person Canceller { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }
    }
}