using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS.api.Model
{
    public class Course : IDatableEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MaxCapcity { get; set; }

        public string SearchableString => $"{Name} {Id}";

        [JsonIgnore]
        public ICollection<User> Students { get; set; }

        [JsonIgnore]
        public ICollection<Module> Modules { get; set; }

    }
}
