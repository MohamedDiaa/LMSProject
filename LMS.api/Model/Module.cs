using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.api.Model
{
    public class Module : IDatableEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int CourseID { get; set; }

        [JsonIgnore]
        public Course Course { get; set; }

        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; }

        public string SearchableString => $"{Title} {Id}";
    }
}
