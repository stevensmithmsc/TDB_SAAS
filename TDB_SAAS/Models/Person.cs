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

        public Nullable<int> FinanceCode { get; set; }
        public virtual CostCentre Finance { get; set; }

        public Nullable<int> SubjectiveID { get; set; }
        public virtual Subjective JobStatus { get; set; }

        [Display(Name = "Line Manager")]
        public Nullable<int> LineManID { get; set; }
        public virtual Person LineManager { get; set; }

        public virtual ICollection<Person> Minions { get; set; }

        public Nullable<int> CohortID { get; set; }
        public virtual Cohort Cohort { get; set; }

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
        public virtual ICollection<Course> CoursesCreated { get; set; }
        public virtual ICollection<CFlag> CFlagsCreated { get; set; }
        public virtual ICollection<Session> SessionsCreated { get; set; }
        public virtual ICollection<Location> LocationsCreated { get; set; }
        public virtual ICollection<Status> StatusesCreated { get; set; }
        public virtual ICollection<Requirement> RequirementsCreated { get; set; }
        public virtual ICollection<Attendance> AttendancesBooked { get; set; }
        public virtual ICollection<Team> TeamsCreated { get; set; }
        public virtual ICollection<TeamMember> TeamMembersCreated { get; set; }
        public virtual ICollection<Borough> BoroughsCreated { get; set; }
        public virtual ICollection<CostCentre> CostCentresCreated { get; set; }
        public virtual ICollection<Subjective> SubjectivesCreated { get; set; }
        public virtual ICollection<Service> ServicesCreated { get; set; }
        public virtual ICollection<Cohort> CohortsCreated { get; set; }

        public DateTime Created { get; set; }

        
        public int ModifierID { get; set; }
        public virtual Person Modifier { get; set; }

        public virtual ICollection<Person> PeopleModified { get; set; }
        public virtual ICollection<Title> TitlesModified { get; set; }
        public virtual ICollection<Flag> FlagsModified { get; set; }
        public virtual ICollection<Course> CoursesModified { get; set; }
        public virtual ICollection<CFlag> CFlagsModified { get; set; }
        public virtual ICollection<Session> SessionsModified { get; set; }
        public virtual ICollection<Location> LocationsModified { get; set; }
        public virtual ICollection<Status> StatusesModified { get; set; }
        public virtual ICollection<Requirement> RequirementsModified { get; set; }
        public virtual ICollection<Attendance> AttendancesCancelled { get; set; }
        public virtual ICollection<Attendance> AttendancesModified { get; set; }
        public virtual ICollection<Team> TeamsModified { get; set; }
        public virtual ICollection<TeamMember> TeamMembersModified { get; set; }
        public virtual ICollection<Borough> BoroughsModified { get; set; }
        public virtual ICollection<CostCentre> CostCentresModified { get; set; }
        public virtual ICollection<Subjective> SubjectivesModified { get; set; }
        public virtual ICollection<Service> ServicesModified { get; set; }
        public virtual ICollection<Cohort> CohortsModified { get; set; }

        public DateTime Modified { get; set; }

        public virtual ICollection<Flag> Flags { get; set; }

        public virtual ICollection<Session> SessionsTrained { get; set; }

        public virtual ICollection<Requirement> Requirements { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual ICollection<Team> LeaderOf { get; set; }

        public virtual ICollection<TeamMember> MemberOf { get; set; }

        public virtual ICollection<Borough> Boroughs { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public virtual string FullName {  get { return GetFullName(); } }


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
            CoursesCreated = new HashSet<Course>();
            CoursesModified = new HashSet<Course>();
            CFlagsCreated = new HashSet<CFlag>();
            CFlagsModified = new HashSet<CFlag>();
            SessionsCreated = new HashSet<Session>();
            SessionsModified = new HashSet<Session>();
            SessionsTrained = new HashSet<Session>();
            LocationsCreated = new HashSet<Location>();
            LocationsModified = new HashSet<Location>();
            StatusesCreated = new HashSet<Status>();
            StatusesModified = new HashSet<Status>();
            Requirements = new HashSet<Requirement>();
            RequirementsCreated = new HashSet<Requirement>();
            RequirementsModified = new HashSet<Requirement>();
            Attendances = new HashSet<Attendance>();
            AttendancesBooked = new HashSet<Attendance>();
            AttendancesCancelled = new HashSet<Attendance>();
            AttendancesModified = new HashSet<Attendance>();
            TeamsCreated = new HashSet<Team>();
            TeamsModified = new HashSet<Team>();
            LeaderOf = new HashSet<Team>();
            MemberOf = new HashSet<TeamMember>();
            TeamMembersCreated = new HashSet<TeamMember>();
            TeamMembersModified = new HashSet<TeamMember>();
            Boroughs = new HashSet<Borough>();
            BoroughsCreated = new HashSet<Borough>();
            BoroughsModified = new HashSet<Borough>();
            CostCentresCreated = new HashSet<CostCentre>();
            CostCentresModified = new HashSet<CostCentre>();
            SubjectivesCreated = new HashSet<Subjective>();
            SubjectivesModified = new HashSet<Subjective>();
            Services = new HashSet<Service>();
            ServicesCreated = new HashSet<Service>();
            ServicesModified = new HashSet<Service>();
            CohortsCreated = new HashSet<Cohort>();
            CohortsModified = new HashSet<Cohort>();
        }

        public string GetFullName()
        {
            return (PreferredName == null||PreferredName == "") ? ((Title != null) ? Title.TitleValue + " " : "") + (Forename != null?Forename + " ":"") + Surname : PreferredName;
        }

        public Team GetMainTeam()
        {
            var mainTeam = MemberOf.Where(m => m.Active && m.Main).Select(m => m.Team).SingleOrDefault();
            return mainTeam == null ? null : mainTeam;
        }
    }
}