using IzikoMuseumWebsite.Data;
using IzikoMuseumWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzikoMuseumWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        // =========================
        // LOGIN - GET
        // =========================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // =========================
        // LOGIN - POST
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            string email,
            string password)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Please enter your email and password.";
                return View();
            }

            email = email.Trim().ToLower();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password);

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            // Store user information in session
            HttpContext.Session.SetString(
                "UserId",
                user.UserId.ToString());

            HttpContext.Session.SetString(
                "UserEmail",
                user.Email);

            HttpContext.Session.SetString(
                "UserName",
                user.FullName);

            HttpContext.Session.SetString(
                "UserRole",
                user.Role);

            // Record login activity
            var activity = new UserActivity
            {
                UserId = user.UserId,
                ActivityType = "Login",
                Description = "User logged into the Iziko Museum website.",
                ActivityDate = DateTime.Now
            };

            _context.UserActivities.Add(activity);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // =========================
        // REGISTER - GET
        // =========================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // =========================
        // REGISTER - POST
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            string fullName,
            string email,
            string password,
            string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                ViewBag.Error = "Please complete all fields.";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            if (password.Length < 6)
            {
                ViewBag.Error =
                    "Password must contain at least 6 characters.";

                return View();
            }

            email = email.Trim().ToLower();

            // Check if email already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email);

            if (existingUser != null)
            {
                ViewBag.Error =
                    "An account with this email already exists.";

                return View();
            }

            // Create new visitor
            var user = new User
            {
                FullName = fullName.Trim(),
                Email = email,
                Role = "Visitor",
                CreatedAt = DateTime.Now
            };

            // Hash the password
            user.PasswordHash =
                _passwordHasher.HashPassword(user, password);

            _context.Users.Add(user);

            // Record registration activity
            var activity = new UserActivity
            {
                User = user,
                ActivityType = "Registration",
                Description =
                    "New visitor registered on the Iziko Museum website.",
                ActivityDate = DateTime.Now
            };

            _context.UserActivities.Add(activity);

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Registration successful. You can now log in.";

            return RedirectToAction("Login");
        }

        // =========================
        // LOGOUT
        // =========================

        public async Task<IActionResult> Logout()
        {
            var userIdString =
                HttpContext.Session.GetString("UserId");

            if (int.TryParse(userIdString, out int userId))
            {
                var activity = new UserActivity
                {
                    UserId = userId,
                    ActivityType = "Logout",
                    Description =
                        "User logged out of the Iziko Museum website.",
                    ActivityDate = DateTime.Now
                };

                _context.UserActivities.Add(activity);

                await _context.SaveChangesAsync();
            }

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}