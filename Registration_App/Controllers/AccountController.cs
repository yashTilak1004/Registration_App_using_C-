/*
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Registration_App.Entities;
using Registration_App.Models;
using System.Security.Claims;

namespace Registration_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext appDbcontext)
        {
            _context = appDbcontext;
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
                    Password = model.Password,
                    UserName = model.UserName
                };

                try
                {
                    _context.Full_table.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully.";
                }
                catch (Exception)
                {
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

        //through the model posts
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Full_table.Where(x => (x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail) && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.Email),
                        new Claim("Name",user.FirstName),
                        new Claim("Email",user.Email),
                        new Claim(ClaimTypes.Role,"User")
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
            return View();
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
*/

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

        

         

        /*
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
                    Role   = model.Role
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
                catch (Exception)
                {
                    ModelState.AddModelError("", "Please enter unique email or password.");
                    return View(model);
                }
            }
            return View(model);
        }
        */

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
