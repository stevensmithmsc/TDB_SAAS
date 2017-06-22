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

        [ForeignKey("Title")]
        [Display(Name = "Title")]
        public Nullable<short> TitleID { get; set; }
        public virtual Title Title { get; set; }

        [Column("FName", TypeName = "varchar")]
        [MaxLength(35)]
        public string Forename { get; set; }

        [Required]
        [Column("SName", TypeName = "varchar")]
        [MaxLength(35)]
        public string Surname { get; set; }

        [Column("PName", TypeName = "varchar")]
        [MaxLength(70)]
        [Display(Name = "Preferred Name")]
        public string PreferredName { get; set; }

        public Nullable<Gender> Gender { get; set; }

        [Column("JobTitle", TypeName = "varchar")]
        [MaxLength(100)]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        //public Nullable<int> Finance { get; set; }

        //public Nullable<int> JobStatus { get; set; }

        [ForeignKey("LineManager")]
        [Display(Name = "Line Manager")]
        public Nullable<int> LineManID { get; set; }
        public virtual Person LineManager { get; set; }

        //public Nullable<int> CohortID { get; set; }

        //public string UUID { get; set; }

        [Column("Phone", TypeName = "varchar")]
        [MaxLength(20)]
        [Display(Name = "Telephone Number")]
        public string Phone { get; set; }

        [Column("Email", TypeName = "varchar")]
        [MaxLength(80)]
        [Display(Name = "E-Mail Address")]
        public string Email { get; set; }

        //public bool LM { get; set; }

        //public bool Trainer { get; set; }

        //public bool LeftTrust { get; set; }

        //public bool NoTraining { get; set; }

        //public bool Bank { get; set; }

        //public bool External { get; set; }

        public Nullable<int> ESRID { get; set; }

        [Column("MName", TypeName = "varchar")]
        [MaxLength(35)]
        [Display(Name ="Middle Name(s)")]
        public string MiddleName { get; set; }

        [Column("Comments", TypeName = "text")]
        public string Comments { get; set; }

        [ForeignKey("Creator")]
        public int? CreatorID { get; set; }
        public virtual Person Creator { get; set; }

        public DateTime Created { get; set; }

        [ForeignKey("Modifier")]
        public int? ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public DateTime Modified { get; set; }

        public string GetFullName()
        {
            return (PreferredName == null||PreferredName == "") ? ((Title != null) ? Title.TitleValue + " " : "") + (Forename != null?Forename + " ":"") + Surname : PreferredName;
        }
    }
}