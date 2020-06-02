using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MaasVallei.Entities;
using MaasVallei.Models;
using MaasVallei.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaasVallei.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly PasswordHashService _passwordHashService;

        public AccountController(UserService userService, PasswordHashService passwordHashService)
        {
            _userService = userService;
            _passwordHashService = passwordHashService;
        }
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// On POST extract credentials out of incoming model.
        /// Does this user exist in the database? Login user.
        /// Register a cookie with user values and return the view.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            if (!ModelState.IsValid) return View();

            var user = _userService.Get().FirstOrDefault(u =>
                u.Username.Equals(model.Username) && u.PasswordHash.Equals(_passwordHashService.Hash(model.Password)));

            if (user == null) return View(model);

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.UserData, user.Id)
            };

            var identity = new ClaimsIdentity(userClaims, "User Identity");

            var userPrincipal = new ClaimsPrincipal(new []{ identity});
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Panel", "Home");
        }

        /// <summary>
        /// Logout the current user and destroy the registered cookie.
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Check if user with same credentials already exists
        /// If not create new user and redirect to login action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(RegisterUserModel model)
        {
            if (!ModelState.IsValid) return View();

            var user = _userService.Get().FirstOrDefault(u =>
            u.Username == model.Username && u.EmailAddress == model.EmailAddress);

            if (user != null) return View();

            _userService.Create(new User
            {
                EmailAddress = model.EmailAddress,
                PasswordHash = _passwordHashService.Hash(model.Password),
                Username = model.Username,
                Role = "USER"
            });

            var loginUser = new UserModel
            {
                EmailAddress = model.EmailAddress,
                Password = model.Password,
                Username = model.Username
            };

            return Login(loginUser);
        }

    }
}