using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class PersonFormViewModel
    {
        public IEnumerable<Title> Titles { get; set; }

        public Person Person { get; set; }

        public int ID { get; set; }

        [Display(Name = "Title")]
        public Nullable<short> TitleID { get; set; }

        [MaxLength(35)]
        public string Forename { get; set; }

        [Required]
        [MaxLength(35)]
        public string Surname { get; set; }

        [MaxLength(70)]
        [Display(Name = "Preferred Name")]
        public string PreferredName { get; set; }

        public Nullable<Gender> Gender { get; set; }

        [MaxLength(100)]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Line Manager")]
        public Nullable<int> LineManID { get; set; }

        [MaxLength(20)]
        [Display(Name = "Telephone Number")]
        public string Phone { get; set; }

        [MaxLength(80)]
        [Display(Name = "E-Mail Address")]
        public string Email { get; set; }

        public Nullable<int> ESRID { get; set; }

        [MaxLength(35)]
        [Display(Name = "Middle Name(s)")]
        public string MiddleName { get; set; }

        public string Comments { get; set; }

        public FlagSelector[] Flags { get; set; }

        public string DisplayName { get; set; }



        public PersonFormViewModel()
        {

        }

        public PersonFormViewModel(Person person, IEnumerable<Title> Titles, IEnumerable<Flag> AllFlags)
        {
            this.Titles = Titles;
            this.ID = person.ID;
            this.TitleID = person.TitleID;
            this.Forename = person.Forename;
            this.Surname = person.Surname;
            this.PreferredName = person.PreferredName;
            this.DisplayName = person.GetFullName();
            this.Gender = person.Gender;
            this.JobTitle = person.JobTitle;
            this.LineManID = person.LineManID;
            this.Phone = person.Phone;
            this.Email = person.Email;
            this.ESRID = person.ESRID;
            this.MiddleName = person.MiddleName;
            this.Comments = person.Comments;
            this.Flags = new FlagSelector[AllFlags.Count()];
            int i = 0;
            foreach (Flag f in AllFlags)
            {
                FlagSelector fs = new FlagSelector(f);
                fs.Selected = person.Flags.Contains(f);
                Flags[i] = fs;
                i++;
            }

        }

        public class FlagSelector
        {
            public Flag Flag { get; set; }
            public bool Selected { get; set; }

            public FlagSelector(Flag f)
            {
                this.Flag = f;
                this.Selected = false;
            }

            public FlagSelector()
            {
            }
        }
    }
}