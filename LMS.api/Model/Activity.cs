using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS.api.Model
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
  
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
        public int ModuleID { get; set; }

        [JsonIgnore]
        public Module Module { get; set; }
        
    }
}
