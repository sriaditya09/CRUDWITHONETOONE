using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication3.Model.Entities
{
    public class Address
    {
        [Key]
        public int ID { get; set; }

        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        // Foreign Key to Student
        public int StudentId { get; set; }

        // Navigation property to Student
        [JsonIgnore]
        public Student? Student { get; set; }
    }
}
