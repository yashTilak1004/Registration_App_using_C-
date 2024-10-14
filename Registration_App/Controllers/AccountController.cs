using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity; // Add this using directive
using Registration_App.Entities;
using Registration_App.Models;
using System.Security.Claims;

namespace Registration_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<UA> _passwordHasher;

        public AccountController(AppDbContext appDbcontext)
        {
            _context = appDbcontext;
            _passwordHasher = new PasswordHasher<UA>();
        }

        public IActionResult Index()
        {
            return View(_context.Full_table.ToList());
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UA account = new UA
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Skills = model.Skills,
                    Role = model.Role
                };

                // Hash the password before saving
                account.Password = _passwordHasher.HashPassword(account, model.Password);

                try
                {
                    _context.Full_table.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully.";
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    ModelState.AddModelError("", "Please enter unique email or password.");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Full_table
                    .FirstOrDefault(x => x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail);

                if (user != null)
                {
                    // Verify the password
                    var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Email),
                            new Claim("Name", user.FirstName),
                            new Claim("Email", user.Email),
                            new Claim(ClaimTypes.Role, "User")
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return RedirectToAction("SecurePage");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username/Email or Password is not correct.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email or Password is not correct.");
                }
            }
            return View();
        }

        /*
        [HttpPost]
        public IActionResult Update(int Id)
        {
            var Id1 = _context.Full_table.FirstOrDefault(x => x.Id == Id);
            var model = new RegistrationViewModel
            {
                Email = Id1.Email,
                FirstName = Id1.FirstName,      
                
            };
            

            return View();
        }*/

        /*
        [HttpGet]
        public IActionResult Update(int id)
        {
            var user = _context.Full_table.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new RegistrationViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Skills = user.Skills,
                Role = user.Role
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Full_table.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                // Update user properties
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.DateOfBirth = model.DateOfBirth;

                user.Gender = user.Gender;

                user.Skills = model.Skills;

                user.Role = user.Role;//

                // Optionally, hash the password if it was changed
                if (!string.IsNullOrEmpty(model.Password))
                {
                    user.Password = _passwordHasher.HashPassword(user, model.Password);
                }

                try
                {
                    _context.SaveChanges();
                    ViewBag.Message = $"{user.FirstName} {user.LastName} updated successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "There was a problem updating the account.");
                    return View(model);
                }
            }
            return View(model);
        }
        */

        [HttpGet]
        public IActionResult Update(int id)
        {
            var user = GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new RegistrationViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Skills = user.Skills,
                Role = user.Role
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Update(int id, RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            UpdateUserProperties(user, model);

            // Handle password change if provided
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = _passwordHasher.HashPassword(user, model.Password);
            }

            try
            {
                _context.SaveChanges();
                ViewBag.Message = $"{user.FirstName} {user.LastName} updated successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception details
                ModelState.AddModelError("", "There was a problem updating the account. Please try again.");
                return View(model);
            }
        }

        private UA GetUserById(int id)
        {
            return _context.Full_table.FirstOrDefault(x => x.Id == id);
        }

        private void UpdateUserProperties(UA user, RegistrationViewModel model)
        {
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.DateOfBirth = model.DateOfBirth;
            user.Gender = model.Gender; // Assuming this should be updated
            user.Skills = model.Skills;
            user.Role = model.Role;
        }






        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
