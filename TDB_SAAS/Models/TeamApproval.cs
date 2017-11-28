using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class TeamApproval
    {
        public int ID { get; set; }

        public int StaffID { get; set; }
        public virtual Person Staff { get; set; }

        public string Team { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Details { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }
    }
}