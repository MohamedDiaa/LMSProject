namespace LMS.api.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Start {  get; set; }

        public DateTime End { get; set; }

        public ICollection<User> Students { get; set; }
    }
}
