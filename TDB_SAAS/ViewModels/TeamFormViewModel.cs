﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TDB_SAAS.Models;

namespace TDB_SAAS.ViewModels
{
    public class TeamFormViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        //public string ESR { get; set; }

        [Display(Name = "Cost Code")]
        public Nullable<int> FinanceCode { get; set; }

        [Display(Name = "Leader")]
        public Nullable<int> LeaderID { get; set; }

        [Display(Name = "Cohort")]
        public Nullable<int> CohortID { get; set; }

        [Display(Name = "No Training Required")]
        public bool NoTrain { get; set; }

        public TeamMember[] Members { get; set; }

        public BoroughSelector[] Boroughs { get; set; }

        public TeamFormViewModel()
        {
        }

        public TeamFormViewModel(Team team, IEnumerable<Borough> AllBoros)
        {
            this.ID = team.ID;
            this.TeamName = team.TeamName;
            this.FinanceCode = team.FinanceCode;
            this.LeaderID = team.LeaderID;
            this.CohortID = team.CohortID;
            this.NoTrain = team.NoTrain;
            this.Members = team.Members.ToArray();
            this.Boroughs = new BoroughSelector[AllBoros.Count()];
            int i = 0;
            foreach (Borough b in AllBoros)
            {
                BoroughSelector bs = new BoroughSelector(b);
                bs.Selected = team.Boroughs.Contains(b);
                Boroughs[i] = bs;
                i++;
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
    }
}