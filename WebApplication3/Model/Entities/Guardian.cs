using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication3.Model.Entities
{
    public class Guardian
    {
        [Key]
        public int ID { get; set; }

        public string? Name { get; set; }
        public string? Relation { get; set; }

        // Foreign Key to Student
        public int StudentId { get; set; }

        // Navigation property to Student
        [JsonIgnore]
        public Student? Student { get; set; }
    }
}
