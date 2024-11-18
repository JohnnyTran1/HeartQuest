using Microsoft.AspNetCore.Mvc;
using DatingSiteTeamProject.Models;
using System.Data.SqlClient;
using Utilities;
using System.Data.Common;
using System.Reflection;
using System.Data;
using DatingSiteTeamProject.Helpers;
using System.Text;

namespace DatingSiteTeamProject.Controllers
{
    public class RegistrationController : Controller
    {
        RegistrationDBModel registration = new RegistrationDBModel();
        const string CookieUserId = "UserId";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration_View()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUserAccount(UserModel user)
        {
            //check to see if userModel object is passed
            if (user != null && ModelState.IsValid)
            {
                int userId = registration.RegisterUser(user);

                // Check if registration was successful
                if (userId > 0)
                {
                    CookieOptions options = new CookieOptions();
                    //options.Expires = DateTime.Now.AddDays(7);
                    options.HttpOnly = true;
                    // options.Secure = true; 

                    Response.Cookies.Append(CookieUserId, userId.ToString(), options);

                    ViewData["ValidationMessage"] = "Successfully created User";
                    return RedirectToAction("InsertProfile_View", "Profile", new { id = userId });
                }
                else if (userId == -1)
                {
                    ViewData["ErrorMessage"] = "Username already exist. Please choose another one.";
                    return View("Registration_View", user);
                }

            }

            return View("Registration_View", user);
        }

        public IActionResult CheckUserCookie()
        {
            // Example method to retrieve and use cookie data

            string userId = Request.Cookies[CookieUserId];

            if (!string.IsNullOrEmpty(userId))
            {
                // Use the cookie data as needed
                return View("UserProfile", new { id = userId }); // Adjust as necessary
            }

            return RedirectToAction("Registration_View");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete(CookieUserId);

            return RedirectToAction("Index", "Home");
        }

    }
}
