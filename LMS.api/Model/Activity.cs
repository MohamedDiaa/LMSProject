using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS.api.Model
{
    public class Activity : IDatableEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int ModuleID { get; set; }

        public string SearchableString => $"{Title} {Id}";

        [JsonIgnore]
        public Module Module { get; set; }
    }
}
