using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM.repository.Repository;
using CI_PLATFORM_.repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_OF_CI_PLATFORM.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Transactions;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserInterface _iuserRepository;

        public HomeController(ILogger<HomeController> logger, IUserInterface iuserRepository)
        {
            _iuserRepository = iuserRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {   
            return View();
        }

        public IActionResult LOGIN()

        {
            if(HttpContext.Session.GetString("username")!=null){ 
            
            return RedirectToAction ("platformLanding","Mission");
            }
       
            return View();
        }
        
        [HttpPost]
        public IActionResult LOGIN(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                
                var entity = _iuserRepository.login(user);
                if (entity == "User Does not Exists" )
                {
                    ModelState.AddModelError("Email", entity);
                    return View("LOGIN");
                }
                if (entity == "invalid password")
                {
                    ModelState.AddModelError("Password", entity);
                    return View("LOGIN");
                }
               /* var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Email) },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);*/
                var Users = entity.Split(',');
             
                HttpContext.Session.SetString("username", Users[0]);
                HttpContext.Session.SetString("userid", Users[1]);
                TempData["LOGIN"] = "successfully logged in";
                return RedirectToAction("PlatformLanding", "Mission");
            }
            return View("LOGIN");
        }

        public IActionResult LogOut()
        {
/*            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
*/           
            HttpContext.Session.Clear();
            return RedirectToAction("PlatformLanding", "Mission");
        }

        public IActionResult FORGOTPASSWORD()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FORGOTPASSWORD(forgotviewmodel user)
        {
            if (ModelState.IsValid)
            {
                var entity = _iuserRepository.FORGOTPASSWORD(user);
                if (entity == null)
                {
                    ModelState.AddModelError("Email", "User not found");
                    return RedirectToAction("FORGOTPASSWORD");
                }
                HttpContext.Session.SetString("Token", entity);
                TempData["FORGOTPASSWORD"] = "Link Send to Mail";
                return View();
            }
            return View();
        }

        public IActionResult RESETPAGE()
        {
           if(HttpContext.Session.GetString("Token")== null)
            {
                return NotFound("Link Expired");
            }
            return View();
        }

        [HttpPost]
        public IActionResult RESETPAGE(resetviemodel entry)
        {
            if (ModelState.IsValid)
            {
               
                var token = HttpContext.Session.GetString("Token");
                var entity = _iuserRepository.RESETPAGE(entry, token);
                if (entity == null)
                {
                    ModelState.AddModelError("", "invalid user");
                    return RedirectToAction("RESETPAGE");
                }
                if (entity.Equals("Confirm password is not matching with password"))
                {
                    ModelState.AddModelError("ConfirmPassword", entity);
                    return RedirectToAction("RESETPAGE");
                }
                HttpContext.Session.Remove(token);
                TempData["RESETPAGE"] = "Password Changed Successfully";
                return RedirectToAction("LOGIN");
            }
            return View();
        }

        public IActionResult REGISTRATIONPAGE()
        {
            return View();
        }

        [HttpPost]
        public IActionResult REGISTRATIONPAGE(registerviewmodel user )
        {
            
            if (ModelState.IsValid)
            {
                var entity = _iuserRepository.REGISTRATIONPAGE(user);
                if (entity == "user already exist")
                {
                    ModelState.AddModelError("Email",entity);
                    return View();
                }
                TempData["REGISTRATIONPAGE"] = "Successfully Registered";
                return RedirectToAction("LOGIN");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserEdit()
        {
            var userid = HttpContext.Session.GetString("userid");
            EditUserViewModel model = _iuserRepository.getUserDetails(long.Parse(userid));
            return View(model);
        }

        [HttpPost]
        public IActionResult UserEdit(EditUserViewModel model)
        {

            var userid = HttpContext.Session.GetString("userid");
            _iuserRepository.editUserDetails(model, long.Parse(userid));
            TempData["success"] = "Profile updated successfully";
            return RedirectToAction("UserEdit");
        }
        public IActionResult ContactUs(EditUserViewModel model)
        {
            var userid = HttpContext.Session.GetString("userid");
            _iuserRepository.getcontact(model, long.Parse(userid));
            return RedirectToAction("UserEdit");


        }
        public void addskill(List<int> skillids)
        {
            var userid = HttpContext.Session.GetString("userid");
            _iuserRepository.addskill(skillids, userid);
        }

        public IActionResult changePass(EditUserViewModel model)
        {
            var userid = HttpContext.Session.GetString("userid");
            var result = _iuserRepository.changepass(model, userid);
            if (result == "success")
            {
                TempData["success"] = "Password Updated Successfully";
                return RedirectToAction("UserEdit", "Home");
            }
            else
            {
                TempData["error"] = "password is updated";
                return RedirectToAction("UserEdit", "Home");

            }

        }
        [HttpPost]
        public JsonResult City(int id)
        {
            var city = _iuserRepository.GetCities(id);
            return Json(city);
        }

    }
}