﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class PersonFormViewModel
    {
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

        [Display(Name = "Finance")]
        public Nullable<int> FinanceCode { get; set; }

        [Display(Name = "Job Status")]
        public Nullable<int> SubjectiveID { get; set; }

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

        [Display(Name = "Cohort")]
        public Nullable<int> CohortID { get; set; }

        public BoroughSelector[] Boroughs { get; set; }

        public MHC[] MHCs { get; set; }

        public Service[] Services { get; set; }

        public TeamMember[] Memberships { get; set; }


        public PersonFormViewModel()
        {

        }

        public PersonFormViewModel(Person person, IEnumerable<Flag> AllFlags, IEnumerable<Borough> AllBoros, IEnumerable<Service> AllMHC)
        {
            this.ID = person.ID;
            this.TitleID = person.TitleID;
            this.Forename = person.Forename;
            this.Surname = person.Surname;
            this.PreferredName = person.PreferredName;
            this.DisplayName = person.GetFullName();
            this.Gender = person.Gender;
            this.JobTitle = person.JobTitle;
            this.FinanceCode = person.FinanceCode;
            this.SubjectiveID = person.SubjectiveID;
            this.LineManID = person.LineManID;
            this.Phone = person.Phone;
            this.Email = person.Email;
            this.ESRID = person.ESRID;
            this.MiddleName = person.MiddleName;
            this.Comments = person.Comments;
            this.CohortID = person.CohortID;
            this.Flags = new FlagSelector[AllFlags.Count()];
            int i = 0;
            foreach (Flag f in AllFlags)
            {
                FlagSelector fs = new FlagSelector(f);
                fs.Selected = person.Flags.Contains(f);
                Flags[i] = fs;
                i++;
            }
            this.Boroughs = new BoroughSelector[AllBoros.Count()];
            i = 0;
            foreach (Borough b in AllBoros)
            {
                BoroughSelector bs = new BoroughSelector(b);
                bs.Selected = person.Boroughs.Contains(b);
                Boroughs[i] = bs;
                i++;
            }
            this.MHCs = new MHC[AllMHC.Count()];
            i = 0;
            foreach (Service s in AllMHC)
            {
                MHC mhc = new MHC(s);
                mhc.Selected = person.Services.Contains(s);
                MHCs[i] = mhc;
                i++;
            }
            this.Services = person.Services.Where(s => s.Level == ServiceLevel.Speciality && s.Display).ToArray();
            this.Memberships = person.MemberOf.ToArray();
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

        public class BoroughSelector
        {
            public Borough Boro { get; set; }
            public bool Selected { get; set; }

            public BoroughSelector(Borough b)
            {
                this.Boro = b;
                this.Selected = false;
            }

            public BoroughSelector()
            {
            }
        }

        public class MHC
        {
            public Service Service { get; set; }
            public bool Selected { get; set; }

            public MHC(Service s)
            {
                this.Service = s;
                this.Selected = false;
            }

            public MHC()
            {
            }
        }
    }
}