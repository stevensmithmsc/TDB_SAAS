using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB_SAAS.Models
{
    public class RA
    {
        public int StaffID { get; set; }
        public virtual Person Staff {get; set; }

        public string UUID { get; set; }

        [Display(Name = "PDS Role")]
        public Nullable<short> PDSRoleID { get; set; }
        public virtual Status PDSRole { get; set; }

        [Display(Name = "PLUS Updated")]
        public Nullable<short> PLUSUpdatedID { get; set; }
        public virtual Status PlusUpdated { get; set; }

        [Display(Name = "ESR Updated")]
        public Nullable<short> ESRUpdatedID { get; set; }
        public virtual Status ESRUpdated { get; set; }
       
        public Nullable<DateTime> EGifL3 { get; set; }

        public Nullable<DateTime> Declaration { get; set; }

        [Display(Name = "Go Live Approved")]
        public Nullable<DateTime> GoLiveApproved { get; set; }

        public bool GLALocked { get; set; }

        [Display(Name = "Child Health Go Live Approved")]
        public Nullable<DateTime> CHGoLiveAprv { get; set; }

        public bool CHGLALocked { get; set; }

        [Display(Name = "Account Created")]
        public Nullable<DateTime> AccountCreated { get; set; }

        [Display(Name = "Added to Citrix Group")]
        public Nullable<DateTime> AddCITRIX { get; set; }

        [Display(Name = "Password Emailed")]
        public Nullable<DateTime> PasswordEmailed { get; set; }

        [Display(Name = "Access to Plus")]
        public Nullable<DateTime> AccessToPlus { get; set; }

        [Display(Name = "UUID added to ESR")]
        public Nullable<DateTime> UUIDAddESR { get; set; }

        [Display(Name = "Fully Compliant")]
        public Nullable<DateTime> FullyCompliant { get; set; }

        [Display(Name = "RA Comments")]
        public string RAComments { get; set; }

        public int CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }
    }
}