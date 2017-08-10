using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class CostCentre
    {
        public int Code { get; set; }

        public string CCName { get; set; }

        public bool Enbld { get; set; }

        public virtual ICollection<Person> Staff { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public CostCentre()
        {
            Staff = new HashSet<Person>();
            Teams = new HashSet<Team>();
        }
    }
}