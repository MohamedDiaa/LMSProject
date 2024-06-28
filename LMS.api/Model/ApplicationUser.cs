using Microsoft.AspNetCore.Identity;

namespace LMS.api.Model
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow; // Ensure this is set appropriately

        public string Name => $"{FirstName.ToUpperInvariant()} {LastName.ToUpperInvariant()}";
        public string SearchableString => $"{Name.ToUpperInvariant()} {Id}";
    }
}
