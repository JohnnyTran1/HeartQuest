using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Utilities;
using System.Data.Common;
using System.Reflection;
using System.Data;
using DatingSiteTeamProject.Models;
using DatingSiteTeamProject.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatingSiteTeamProject.Controllers
{
    public class LoginController : Controller
    {
        const string CookieUserId = "UserId";
        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey("UserId"))
            {
                return RedirectToAction("ProfileHomeView", "Profile");
            }
            return View("Login_View");
        }



        public IActionResult Login_View()
        {
            var model = new LoginModel();
            model.CheckRememberMe(Request.Cookies);

            if (Request.Cookies.ContainsKey("UserId"))
            {
                return RedirectToAction("ProfileHomeView", "Profile");
            }
            return View("Login_View");
            //if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int userId))
            //{
            //    model = GetUserDetailsById(userId);


            //}
            //return View(model);
            return View(model);
        }

        public IActionResult Logout()
        {
            if (Request.Cookies["RememberMeActive"] == null)
            {
                Response.Cookies.Delete("UserId");
            }
            Response.Cookies.Delete("RememberMeActive");

            return View("Login_View");
        }


        [HttpGet]
        public IActionResult UserLogin(LoginModel login)
        {
            DBConnect objDB = new DBConnect();
            if (login != null && ModelState.IsValid)

                {
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "dbo.LoginHashed";
               // objCommand.CommandText = "dbo.Login";


                SqlParameter paramUsername = new SqlParameter("@Username", login.Username);
          //     SqlParameter paramPassword = new SqlParameter("@Password", user.Password);

                objCommand.Parameters.Add(paramUsername);
          //      objCommand.Parameters.Add(paramPassword);

                SqlParameter paramterID = new SqlParameter("@UserId", SqlDbType.Int);
                paramterID.Direction = ParameterDirection.Output;
                objCommand.Parameters.Add(paramterID);
                    SqlParameter paramHashedPassword = new SqlParameter("@PassReturned", SqlDbType.VarChar, -1);
                      paramHashedPassword.Direction = ParameterDirection.Output;

                     objCommand.Parameters.Add(paramHashedPassword);

                objDB.GetDataSet(objCommand);


                if (objCommand.Parameters["@UserId"].Value != DBNull.Value && objCommand.Parameters["@PassReturned"].Value != DBNull.Value)
                {
                    //return Convert.ToInt32(objCommand.Parameters["@UserId"].Value);
                    login.Id = Convert.ToInt32(objCommand.Parameters["@UserId"].Value);
                    string hashedPassword = Convert.ToString(objCommand.Parameters["@PassReturned"].Value);
                    bool correctPassword = Hasher.VerifyPassword(login.Password, objCommand.Parameters["@PassReturned"].Value.ToString());

                    int userId = Convert.ToInt32(objCommand.Parameters["@UserId"].Value);

                    if (correctPassword)
                    {
                        CookieOptions options = new CookieOptions();
                        options.HttpOnly = true;

                        if (login.RememberMe)
                        {
                            options.Expires = DateTime.Now.AddDays(7);
                            Response.Cookies.Append("RememberMeActive", "true", options);
                        }
                        //else
                        //{
                        //    options.Expires = DateTime.Now.AddMinutes(30);
                        //}

                        Response.Cookies.Append(CookieUserId, userId.ToString(), options);
                        return RedirectToAction("ProfileHomeView", "Profile");



                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Invalid username or password.";
                        return View("Login_View", login);
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "Invalid username or password.";
                    return View("Login_View", login);
                }
            }
            return View("Login_View");

        }
    }
}
