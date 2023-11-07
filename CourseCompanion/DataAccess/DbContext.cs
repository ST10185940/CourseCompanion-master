using CourseCompanion.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CourseCompanion.DataAccess
{
    public class AppData : DbContext
    {
        public DbSet<user> user { get; set; }

        public DbSet<module> module { get; set; }

        public DbSet<semester> semester { get; set; }

        public string connectionString = "server=localhost;user=root;database=course_companion;port=3306;password=@Dvtech123!;";
        string connectionHome = "server=localhost;user=root;database=course_companion;port=3306;password=;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionHome, new MySqlServerVersion(new Version(8, 0, 31)));
        }
    }
}
