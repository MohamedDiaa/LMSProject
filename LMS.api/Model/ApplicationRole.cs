using Microsoft.AspNetCore.Identity;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    {
    }
    public
        ApplicationRole(string roleName) : base(roleName)
    {
    }

    public static string Admin => "Admin";
    public static string Teacher => "Teacher";

    public static string Student => "Student";

    public string SearchableString => Name.ToUpperInvariant();

    public DateTime Created { get; set; }
}