using LMS.api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LMS.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        private readonly SignInManager<IdentityUser> signInManager;

        private readonly UserManager<IdentityUser> userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }


        // api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
         
                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                // Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, model.Password);
                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                if (result.Succeeded)
                {
                   await signInManager.SignInAsync(user, isPersistent: false);
                  return NoContent(); 
                }
                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
             
            return Ok();
        }



       [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Handle successful login
                    return Ok();
                }
                /*
                if (result.RequiresTwoFactor)
                {
                    // Handle two-factor authentication case
                }
                if (result.IsLockedOut)
                {
                    // Handle lockout scenario
                }
                */
                else
                {
                    // Handle failure
                    return BadRequest("Invalid login attempt.");
                }

        }
    }
}
