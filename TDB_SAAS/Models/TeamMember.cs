using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class TeamMember
    {
        public int ID { get; set; }

        public int StaffID { get; set; }
        public virtual Person Staff { get; set; }

        public int TeamID { get; set; }
        public virtual Team Team { get; set; }

        public bool Main { get; set; }

        public string Assignment { get; set; }

        public bool Active { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

    }
}