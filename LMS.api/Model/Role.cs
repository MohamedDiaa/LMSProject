using Microsoft.AspNetCore.Identity;

namespace LMS.api.Model
{
    public class Role : ApplicationRole, IEntity<string>
    {
        public static string Admin => "Admin";
        public static string Teacher => "Teacher";

        public static string Student => "Student";

        public string SearchableString => Name.ToUpperInvariant();

        public DateTime Created { get; set; }
    }
}
