using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.api.Model
{
    public class User : IdentityUser<int>, IEntity
    {
        public DateTime Created { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Name => $"{FirstName.ToUpperInvariant()} {LastName.ToUpperInvariant()}";
        public string SearchableString => $"{Name.ToUpperInvariant()} {Id}";
        public string Password
        {
            get => PasswordHash ?? string.Empty;
            set => PasswordHash = value ?? throw new ArgumentNullException("Password");
        }

        public int? CourseID { get; set; }

        [JsonIgnore]
        public Course? Course { get; set; }
    }
}
