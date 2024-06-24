using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.api.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        
        public int? CourseID { get; set; }

        [JsonIgnore]
        public Course? Course { get; set; }
    }
}
