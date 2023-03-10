using CourseTask3.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseTask3.Repository
{
    public class CourseTaskDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne<Departament>(s => s.Departament)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartamentID);

            modelBuilder.Entity<Course>()
                 .HasMany<Departament>(c => c.Departaments)
                 .WithMany(d => d.Courses);
            modelBuilder.Entity<Departament>()
                .HasMany<Course>(d => d.Courses)
                .WithMany(c => c.Departaments);

            modelBuilder.Entity<Course>()
                .HasMany<Student>(c => c.Students);
            modelBuilder.Entity<Student>()
                .HasMany<Course>(s => s.Courses);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Student> Students { get; set; }

        public CourseTaskDbContext(DbContextOptions<CourseTaskDbContext> options) : base(options)
        {

        }
    }
}