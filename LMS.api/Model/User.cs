using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.api.Model
{
    public class User : ApplicationUser, IEntity<string>
    {
        public string Password
        {
            get => PasswordHash ?? string.Empty;
            set => PasswordHash = value ?? throw new ArgumentNullException(nameof(Password));
        }

        public int? CourseID { get; set; }
        [JsonIgnore]
        public Course? Course { get; set; }
    }
}
