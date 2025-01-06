using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Model.Entities
{
	public class Student
	{
		[Key]
		public int ID { get; set; }
		public string? Name { get; set; } 
		public string? Phone { get; set; }
		public string? Gender { get; set; }

		public int AddressId { get; set; }
		public Address? Address  { get; set; }
	}
}
