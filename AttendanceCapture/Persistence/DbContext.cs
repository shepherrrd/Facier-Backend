using AttendanceCapture.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Persistence;

public class UniversityContext : IdentityDbContext<User, ApplicationRole, long>
{
    public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
    {      
    }
    public DbSet<User> SystemUsers { get; set; }
    public DbSet<LecturTutor> Lecturers { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<LogActivity> LogActivities { get; set; }
    public DbSet<DBSessions> UserSessions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.seed();

    }
}

