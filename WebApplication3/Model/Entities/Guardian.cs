namespace WebApplication3.Model.Entities
{
	public class Guardian
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }

		// Foreign Key to Student
		public int StudentId { get; set; }

		// Navigation property to Student
		public Student? Student { get; set; }

	}
}
