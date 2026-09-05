using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie_Management_System.Models;
using Movie_Management_System.Services.Interfaces;
using Movie_Management_System.ViewModels;

namespace Movie_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
    LoginViewModel model,
    string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) &&
                    Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(
    ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "No account found with this email."
                );

                return View(model);
            }

            var otp = Random.Shared.Next(100000, 1000000).ToString();

            user.PasswordResetOtp = otp;
            user.PasswordResetOtpExpiry = DateTime.UtcNow.AddMinutes(5);

            await _userManager.UpdateAsync(user);

            var body = $"""
        <h2>Password Reset</h2>
        <p>Your OTP is:</p>
        <h1>{otp}</h1>
        <p>This code will expire in 5 minutes.</p>
        """;

            await _emailService.SendEmailAsync(
                user.Email!,
                "Password Reset OTP",
                body
            );

            return RedirectToAction(
                "VerifyOTP",
                new { email = model.Email }
            );
        }

        [HttpGet]
        public IActionResult VerifyOTP(string email)
        {
            var model = new VerifyOtpViewModel
            {
                Email = email
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyOTP(VerifyOtpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid request.");
                return View(model);
            }

            if (string.IsNullOrEmpty(user.PasswordResetOtp) ||
                user.PasswordResetOtpExpiry == null)
            {
                ModelState.AddModelError("", "OTP is invalid.");
                return View(model);
            }

            if (DateTime.UtcNow > user.PasswordResetOtpExpiry)
            {
                ModelState.AddModelError("", "OTP has expired.");
                return View(model);
            }

            if (user.PasswordResetOtp != model.OTP)
            {
                ModelState.AddModelError("", "Invalid OTP.");
                return View(model);
            }

            return RedirectToAction(
                "ResetPassword",
                new
                {
                    email = model.Email,
                    otp = model.OTP
                });
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string otp)
        {
            var model = new ResetPasswordViewModel
            {
                Email = email,
                OTP = otp
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(
    ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid request.");
                return View(model);
            }

         
            if (user.PasswordResetOtp != model.OTP ||
                user.PasswordResetOtpExpiry == null ||
                DateTime.UtcNow > user.PasswordResetOtpExpiry)
            {
                ModelState.AddModelError(
                    "",
                    "OTP is invalid or expired."
                );

                return View(model);
            }

            var removePasswordResult =
                await _userManager.RemovePasswordAsync(user);

            if (!removePasswordResult.Succeeded)
            {
                foreach (var error in removePasswordResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            var addPasswordResult =
                await _userManager.AddPasswordAsync(
                    user,
                    model.Password
                );

            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            user.PasswordResetOtp = null;
            user.PasswordResetOtpExpiry = null;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Login");
        }


    }
}