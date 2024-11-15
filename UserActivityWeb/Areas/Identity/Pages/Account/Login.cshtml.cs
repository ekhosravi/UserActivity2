// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using UserActivity.Models;
using Microsoft.Extensions.DependencyInjection;
using UserActivity.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace UserActivityWeb.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context; // To access the database
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
                                      UserManager<ApplicationUser> userManager,
                                      ApplicationDbContext context,
                                      IHttpContextAccessor httpContextAccessor,
                                      ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }

            public DateTime? LastLoginDate { get; set; } 
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    // Fetch user information and log login activity
                    var user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        // Log successful login details
                        await LogUserLoginActivity(user.Id, true);
                    }

                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                    // Log failed login attempt
                    var user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        await LogUserLoginActivity(user.Id, false);
                    }

                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }


        private async Task LogUserLoginActivity(int userId, bool isSuccess)
        {
            // Retrieve IP address and browser information from the current HTTP request
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            var browserInfo = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
  
            // Get the failed login attempts from session (if any)
            var failedAttempts = HttpContext.Session.GetInt32("FailedLoginAttempts") ?? 0;

            if (isSuccess)
            {
                // Log the successful login and store the failed attempts count
                var successfulLogin = new UserLogin
                {
                    UserId = userId,
                    LoginDateTime = DateTime.UtcNow,
                    IpAddress = ipAddress,
                    DevBrowserInfo = browserInfo,
                    IsSuccess = true,
                    FailedLoginAttempts = failedAttempts
                };

                _context.UserLogins.Add(successfulLogin);
                await _context.SaveChangesAsync();

                // Reset the failed login attempts count after successful login
                HttpContext.Session.Remove("FailedLoginAttempts");


                var user = await _context.ApplicationUsers.FindAsync(userId);
                if (user != null)
                {
                    user.LastLoginDate = DateTime.UtcNow; // Update LastLoginDate
                    _context.Users.Update(user);  
                    await _context.SaveChangesAsync();  
                }
            }
            else
            {
                // Increment failed login attempts and store in session
                HttpContext.Session.SetInt32("FailedLoginAttempts", failedAttempts + 1);

                // Log the failed login attempt
                var failedLogin = new UserLogin
                {
                    UserId = userId,
                    LoginDateTime = DateTime.UtcNow,
                    IpAddress = ipAddress,
                    DevBrowserInfo = browserInfo,
                    IsSuccess = false,
                    FailedLoginAttempts = 0  // Not needed for failed logins
                };

                _context.UserLogins.Add(failedLogin);
                await _context.SaveChangesAsync();
            }
        }


         
    }
}
