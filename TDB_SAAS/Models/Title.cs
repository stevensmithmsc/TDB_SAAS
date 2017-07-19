using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class Title
    {
        public short ID { get; set; }

        public string TitleValue { get; set; }

        public string AvailGenders { get; set; }

        public Nullable<Gender> DefaultGender { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public List<Gender> AllowedGenders
        {
            get
            {
                List<Gender> genlist = new List<Gender>();
                foreach (string s in AvailGenders.Split(';'))
                {
                    genlist.Add((Gender)Enum.Parse(typeof(Gender), s));
                }
                return genlist;
            }
        }

        public ICollection<Person> People { get; set; }

        public Title()
        {
            this.People = new HashSet<Person>();
        }
    }
}