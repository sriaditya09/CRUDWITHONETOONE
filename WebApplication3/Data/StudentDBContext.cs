using Microsoft.EntityFrameworkCore;
using WebApplication3.Model.Entities;

namespace WebApplication3.Data
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Guardian> Guardians { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-many relationship between Student and Address
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Addresses)             // A Student has many Addresses
                .WithOne(a => a.Student)               // Each Address belongs to one Student
                .HasForeignKey(a => a.StudentId);      // Foreign Key in Address to Student

            // One-to-many relationship between Student and Guardian
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Guardians)            // A Student has many Guardians
                .WithOne(g => g.Student)              // Each Guardian belongs to one Student
                .HasForeignKey(g => g.StudentId);     // Foreign Key in Guardian to Student
        }
    }
}
