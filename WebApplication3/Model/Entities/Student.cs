using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication3.Model.Entities
{
	public class Student
	{
		[Key]
		[JsonIgnore]
		public int ID { get; set; }
		public string? Name { get; set; }
		public string? Phone { get; set; }
		public string? Gender { get; set; }

		// One-to-many relationship: A Student has many Addresses
		public ICollection<Address> Address { get; set; } = new List<Address>();

		// One-to-many relationship: A Student has many Guardians
		public ICollection<Guardian> Guardians { get; set; } = new List<Guardian>();
	}
}

