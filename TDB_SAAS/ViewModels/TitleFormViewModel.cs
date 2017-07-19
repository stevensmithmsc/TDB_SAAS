using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class TitleFormViewModel
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "Title")]
        public string TitleValue { get; set; }
        [Display(Name = "Default Gender")]
        [DefaultGenderInAllowedGenders]
        public Nullable<Gender> DefaultGender { get; set; }
        [DefaultGenderInAllowedGenders]
        public GenderSelector[] Genders { get; set; }

        public TitleFormViewModel(Title title)
        {
            this.ID = title.ID;
            this.TitleValue = title.TitleValue;
            this.DefaultGender = title.DefaultGender;
            Gender[] allGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToArray();
            Genders = new GenderSelector[allGenders.Count()];
            int i = 0;
            foreach(Gender g in allGenders)
            {
                GenderSelector gs = new GenderSelector(g);
                gs.Selected = title.AllowedGenders.Contains(g);
                Genders[i] = gs;
                i++;
            }
        }

        public TitleFormViewModel()
        {
        }

        public class GenderSelector
        {
            public int GID { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }

            public GenderSelector(Gender g)
            {
                this.GID = (int)g;
                this.Value = g.ToString();
                this.Selected = false;
            }

            public GenderSelector()
            {
            }
        }
    }
}