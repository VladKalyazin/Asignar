namespace AsignarDBEntities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AsignarDatabaseModel : DbContext
    {
        public AsignarDatabaseModel()
            : base("name=AsignarDBM")
        {
        }

        public virtual DbSet<DefectPriority> DefectPriorities { get; set; }
        public virtual DbSet<Defect> Defects { get; set; }
        public virtual DbSet<DefectStatus> DefectStatuses { get; set; }
        public virtual DbSet<Filter> Filters { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DefectPriority>()
                .HasMany(e => e.Defects)
                .WithRequired(e => e.DefectPriority)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DefectStatus>()
                .HasMany(e => e.Defects)
                .WithRequired(e => e.DefectStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.Prefix)
                .IsFixedLength();

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Defects)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Projects)
                .Map(m => m.ToTable("ProjectsToUsersBindings").MapLeftKey("ProjectID").MapRightKey("UserID"));

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Defects)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AssigneeUserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Filters)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AssigneeUserID);
        }
    }
}
