namespace EF_CodeFirst_DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=SchoolDB")
        {
        }

        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .Property(e => e.ClassName)
                .IsUnicode(false);

            modelBuilder.Entity<People>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<People>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<People>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.StudentName)
                .IsUnicode(false);
        }
    }
}
