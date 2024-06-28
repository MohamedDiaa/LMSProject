using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS.api.Model
{
    public class Course : IDatableEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
        public DateTime Starts { get; set; } = DateTime.Today;
        public DateTime Ends { get; set; } = DateTime.Today.AddMonths(1);

        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MaxCapcity { get; set; }

        public string SearchableString => $"{Name} {Id}";

        [JsonIgnore]
        public ICollection<ApplicationUser> Students { get; set; }

        [JsonIgnore]
        public ICollection<Module> Modules { get; set; }

    }
}
