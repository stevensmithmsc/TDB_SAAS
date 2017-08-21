using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Cohort
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string CohortDescription { get; set; }

        public decimal Number { get; set; }

        public string Notes { get; set; }

        public ICollection<Person> Staff { get; set; }

        public ICollection<Team> Teams { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public Cohort()
        {
            Staff = new HashSet<Person>();
            Teams = new HashSet<Team>();
        }

    }
}