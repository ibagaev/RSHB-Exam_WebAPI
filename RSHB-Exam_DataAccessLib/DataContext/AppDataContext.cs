using Microsoft.EntityFrameworkCore;
using RSHB_Exam_ModelLib;

namespace RSHB_Exam_DataAccessLib.DataContext
{
    public class AppDataContext : DbContext
    {
        public DbSet<Employee> employees { get; set; }

        public AppDataContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=rshb_exam.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(f => f.FullName).IsUnique();

        }
    }
}
