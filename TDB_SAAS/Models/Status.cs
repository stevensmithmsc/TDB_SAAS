using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Status
    {
        public short ID { get; set; }

        [Display(Name = "Status Description")]
        public string StatusDesc { get; set; }

        public bool Requirement { get; set; }
        public bool Attendance { get; set; }
        [Display(Name = "TNA Outcome")]
        public bool TNA_OUT { get; set; }
        [Display(Name = "RA PDS Role")]
        public bool RA_PDS { get; set; }
        [Display(Name = "RA Plus Updated")]
        public bool RA_PLUS { get; set; }
        [Display(Name = "RA ESR Updated")]
        public bool RA_ESR { get; set; }

        public ICollection<Requirement> RequirementsWithStatus { get; set; }
        public ICollection<Attendance> AttendanceWithStatus { get; set; }
        public ICollection<TNA> TNAsWithStatus { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public Status()
        {
            this.RequirementsWithStatus = new HashSet<Requirement>();
            this.AttendanceWithStatus = new HashSet<Attendance>();
        }
    }
}