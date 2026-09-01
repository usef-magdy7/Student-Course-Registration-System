using Microsoft.EntityFrameworkCore;
using StudentCourseSystem.Models.Entities;

namespace StudentCourseSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseRequest> CourseRequests { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Primary Key for StudentCourse
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            // Unique Constraints
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.CourseCode)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            // Prevent duplicate requests from the same student for the same course
            modelBuilder.Entity<CourseRequest>()
                .HasIndex(cr => new { cr.StudentId, cr.CourseId })
                .IsUnique();

            // Cascade Delete Relationships
            modelBuilder.Entity<CourseRequest>()
                .HasOne(cr => cr.Course)
                .WithMany(c => c.CourseRequests)
                .HasForeignKey(cr => cr.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseRequest>()
                .HasOne(cr => cr.Student)
                .WithMany(s => s.CourseRequests)
                .HasForeignKey(cr => cr.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}