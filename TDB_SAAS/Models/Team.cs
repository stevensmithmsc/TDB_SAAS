using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Team
    {
        public int ID { get; set; }

        public string TeamName { get; set; }

        //public string ESR { get; set; }

        public Nullable<int> FinanceCode { get; set; }
        public virtual CostCentre Finance { get; set; }

        public Nullable<int> LeaderID { get; set; }
        public virtual Person Leader { get; set; }

        //public Nullable<int> CohortID { get; set; }

        [Display(Name = "No Training Required")]
        public bool NoTrain { get; set; }

        public ICollection<TeamMember> Members { get; set; }

        public ICollection<Borough> Boroughs { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public ICollection<Service> Services { get; set; }

        public Team()
        {
            Members = new HashSet<TeamMember>();
            Boroughs = new HashSet<Borough>();
            Services = new HashSet<Service>();
        }
    }
}