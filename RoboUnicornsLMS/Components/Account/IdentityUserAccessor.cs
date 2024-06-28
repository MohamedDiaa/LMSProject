using LMS.api.Model;
using Microsoft.AspNetCore.Identity;

namespace RoboUnicornsLMS.Components.Account
{
    public sealed class IdentityUserAccessor
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IdentityRedirectManager redirectManager;

        public IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
        {
            this.userManager = userManager;
            this.redirectManager = redirectManager;
        }
        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
