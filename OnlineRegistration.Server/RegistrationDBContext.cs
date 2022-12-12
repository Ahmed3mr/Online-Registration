
using Microsoft.EntityFrameworkCore;
using OnlineRegisteration.Shared.Models;

namespace OnlineRegisteration.Server
{
    public class RegistrationDBContext : DbContext
    {

        public RegistrationDBContext(DbContextOptions<RegistrationDBContext> Options) : base(Options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(c => c.DependantCourse)
                .WithMany()
                .HasForeignKey(c => c.CourseDependenceId);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Admin> Admins { get; set; }    
        public DbSet<Classroom> Classrooms { get; set; }    
        public DbSet<Department> Departments { get; set; }  
        public DbSet<Lecturer> Lecturers { get; set;}
        public DbSet<RegPeriod> RegPeriods { get; set; }

    }
}

