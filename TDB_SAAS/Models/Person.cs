using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    [Table("Staff")]
    public class Person
    {
        public int ID { get; set; }

        [Display(Name = "Title")]
        public Nullable<short> TitleID { get; set; }
        public virtual Title Title { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Preferred Name")]
        public string PreferredName { get; set; }

        public Nullable<Gender> Gender { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        //public Nullable<int> Finance { get; set; }

        //public Nullable<int> JobStatus { get; set; }

        [Display(Name = "Line Manager")]
        public Nullable<int> LineManID { get; set; }
        public virtual Person LineManager { get; set; }

        public virtual ICollection<Person> Minions { get; set; }

        //public Nullable<int> CohortID { get; set; }

        //public string UUID { get; set; }

        [Display(Name = "Telephone Number")]
        public string Phone { get; set; }

        [Display(Name = "E-Mail Address")]
        public string Email { get; set; }

        public Nullable<int> ESRID { get; set; }

        [Display(Name ="Middle Name(s)")]
        public string MiddleName { get; set; }

        public string Comments { get; set; }

        
        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public virtual ICollection<Person> PeopleCreated { get; set; }
        public virtual ICollection<Title> TitlesCreated { get; set; }
        public virtual ICollection<Flag> FlagsCreated { get; set; }

        public DateTime Created { get; set; }

        
        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public virtual ICollection<Person> PeopleModified { get; set; }
        public virtual ICollection<Title> TitlesModified { get; set; }
        public virtual ICollection<Flag> FlagsModified { get; set; }

        public DateTime Modified { get; set; }

        public virtual ICollection<Flag> Flags { get; set; }

        public Person()
        {
            Flags = new HashSet<Flag>();
            Minions = new HashSet<Person>();
            PeopleCreated = new HashSet<Person>();
            PeopleModified = new HashSet<Person>();
            TitlesCreated = new HashSet<Title>();
            TitlesModified = new HashSet<Title>();
            FlagsCreated = new HashSet<Flag>();
            FlagsModified = new HashSet<Flag>();
            Created = DateTime.Now;
            Modified = DateTime.Now;

        }

        public string GetFullName()
        {
            return (PreferredName == null||PreferredName == "") ? ((Title != null) ? Title.TitleValue + " " : "") + (Forename != null?Forename + " ":"") + Surname : PreferredName;
        }
    }
}