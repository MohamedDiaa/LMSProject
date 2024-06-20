using System.ComponentModel.DataAnnotations;

namespace LMS.api.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        
        public int? CourseID { get; set; }

        public Course? Course { get; set; }
    }
}
