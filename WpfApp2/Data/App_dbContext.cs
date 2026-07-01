using Microsoft.EntityFrameworkCore;
using StudentTaskSystem.Model;
using WpfApp2.Model;

namespace WpfApp2.Data
{
    public class App_dbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Teacher> Teacher { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("database=final4;Data Source=LEVASCH-TCH-174\\NEWADHAM;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                 .HasMany(s => s.Assignments)
                 .WithOne(a => a.Student)
                 .HasForeignKey(a => a.StudentId)
                 .OnDelete(DeleteBehavior.Cascade);




            base.OnModelCreating(modelBuilder);
        }
    }
}
