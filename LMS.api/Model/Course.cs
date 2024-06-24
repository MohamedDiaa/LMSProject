using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS.api.Model
{
    public class Course
    {
     
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxCapcity { get; set; }

        public DateTime Start {  get; set; }

        public DateTime End { get; set; }

        [JsonIgnore] 
        public ICollection<User> Students { get; set; }
       
        [JsonIgnore]
        public ICollection<Module> Modules { get; set; }

    }
}
