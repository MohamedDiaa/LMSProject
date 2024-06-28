using System.ComponentModel.DataAnnotations;

namespace LMS.api.Model
{
    public class ModuleDTO
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int CourseID { get; set; }

        public Course? Course { get; set; }

        public ICollection<Activity>? Activities { get; set; }
    }
}
