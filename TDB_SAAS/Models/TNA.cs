using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class TNA
    {
        public int StaffID { get; set; }
        public virtual Person Staff {get; set; }

        public Nullable<DateTime> DateReceived { get; set; }

        public Nullable<int> TrainerID { get; set; }
        public virtual Person Trainer { get; set; }

        public Nullable<DateTime> ContactDate { get; set; }

        public string ContactOutcome { get; set; }

        public Nullable<short> OutcomeID { get; set; }
        public virtual Status Outcome { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }
    }
}