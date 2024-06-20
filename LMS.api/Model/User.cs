namespace LMS.api.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public int CourseID { get; set; }

        Course Course { get; set; }
    }
}
