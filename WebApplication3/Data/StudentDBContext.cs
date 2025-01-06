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


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Student>()
				.HasOne(a => a.Address) 
				.WithOne()              
				.HasForeignKey<Address>(a => a.ID);

				}

	}
}
