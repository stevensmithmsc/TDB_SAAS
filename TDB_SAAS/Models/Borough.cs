using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Borough
    {
        public string ID { get; set; }
        
        [Display(Name="Name")]
        public string BoroughName { get; set; }

        [Display(Name = "Long Name")]
        public string LongName { get; set; }

        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        public ICollection<Person> Staff { get; set; }

        public ICollection<Team> Teams { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }
    }
}