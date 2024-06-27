namespace LMS.api.Model
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int maxCapcity { get; set; } //I spelled it wrong intentionally based on the schema. /Martin 
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
