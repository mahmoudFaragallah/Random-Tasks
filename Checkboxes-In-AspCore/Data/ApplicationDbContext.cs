using Checkboxes_In_AspCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkboxes_In_AspCore.ViewModels;

namespace Checkboxes_In_AspCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<StudentCourse>()
                .HasKey(c => new { c.StudentId, c.CourseId });

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(c => c.StudentCourse)
                .HasForeignKey(sc => sc.StudentId);

            builder.Entity<StudentCourse>()
               .HasOne(sc => sc.Course)
               .WithMany(c => c.StudentCourse)
               .HasForeignKey(sc => sc.CourseId);
        }

        public DbSet<Checkboxes_In_AspCore.ViewModels.StudentCourseViewModel> StudentCourseViewModel { get; set; }

    }
}
