using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Humsafar_Mubarak.Data;
using MVC_Humsafar_Mubarak.Interface;
using MVC_Humsafar_Mubarak.Models;
using MVC_Humsafar_Mubarak.ViewModel;

namespace MVC_Humsafar_Mubarak.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProfileRepository _profileRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManger,IProfileRepository profileRepository ,SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManger;
            _profileRepository = profileRepository;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            // Find the user by email
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                // Check if the password is correct
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    // Sign in the user
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        if(await _userManager.IsInRoleAsync(user, UserRoles.Admin))
                        {
                           return RedirectToAction("Index", "Profile");
                        }
                        // Check if the user has a profile
                        var profile = await _profileRepository.GetProfileByUserId(user.Id);
                        if (profile != null)
                        {
                            // Redirect to the Profile Index if a profile exists
                            return RedirectToAction("Index", "Profile");
                        }
                        else
                        {
                            // Redirect to the Dashboard Create if no profile exists
                            return RedirectToAction("Create", "Dashboard");
                        }
                    }
                }
            }

            // Set error message and return to the view if credentials are wrong or user does not exist
            TempData["Error"] = "Wrong Credentials. Please, try again.";
            return View(loginViewModel);
        }



        public IActionResult Register()
        {
            var result = new RegisterViewModel();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This emial address is already in use";
                return View(registerViewModel);
            }
            var newuser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newuser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newuser, UserRoles.User);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
