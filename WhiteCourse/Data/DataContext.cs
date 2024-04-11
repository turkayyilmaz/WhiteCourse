using Microsoft.EntityFrameworkCore;

namespace WhiteCourse.Data;
public class DataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = WhiteCourse.db");
    }
    public DbSet<Student> Students {get; set;}
    public DbSet<Course> Courses {get; set;}
    public DbSet<CourseRegister> CourseRegisters {get; set;}
    public DbSet<Teacher> Teachers {get; set;}
}