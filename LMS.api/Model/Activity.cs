namespace LMS.api.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int ModuleID { get; set; }
        public Module Module { get; set; }
        
    }
}
