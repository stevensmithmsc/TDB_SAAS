namespace TDB_SAAS.Models
{
    using System.Data.Entity;
    using TDB_SAAS.Models.EntityConfigurations;

    public class TrainDB : DbContext
    {
        // Your context has been configured to use a 'TrainDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TDB_SAAS.Models.TrainDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TrainDB' 
        // connection string in the application configuration file.
        public TrainDB()
            : base("name=TrainDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<Flag> Flags { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CFlag> CFlags { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Requirement> Requirements { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<Borough> Boroughs { get; set; }
        public virtual DbSet<CostCentre> CostCentres { get; set; }
        public virtual DbSet<Subjective> Subjectives { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Cohort> Cohorts { get; set; }
        public virtual DbSet<TNA> TNAs { get; set; }
        public virtual DbSet<RA> RAs { get; set; }
        public virtual DbSet<TeamApproval> TeamApprovals { get; set; }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new TitleConfiguration());
            modelBuilder.Configurations.Add(new FlagConfiguration());
            modelBuilder.Configurations.Add(new CourseConfiguration());
            modelBuilder.Configurations.Add(new CFlagConfiguration());
            modelBuilder.Configurations.Add(new SessionConfiguration());
            modelBuilder.Configurations.Add(new LocationConfiguration());
            modelBuilder.Configurations.Add(new StatusConfiguration());
            modelBuilder.Configurations.Add(new RequirementConfiguration());
            modelBuilder.Configurations.Add(new AttendanceConfiguration());
            modelBuilder.Configurations.Add(new TeamConfiguration());
            modelBuilder.Configurations.Add(new TeamMemberConfiguration());
            modelBuilder.Configurations.Add(new BoroughConfiguration());
            modelBuilder.Configurations.Add(new CostCentreConfiguration());
            modelBuilder.Configurations.Add(new SubjectiveConfiguration());
            modelBuilder.Configurations.Add(new ServiceConfiguration());
            modelBuilder.Configurations.Add(new CohortConfiguration());
            modelBuilder.Configurations.Add(new TNAConfiguration());
            modelBuilder.Configurations.Add(new RAConfiguration());
            modelBuilder.Configurations.Add(new TeamApprovalConfiguration());
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}