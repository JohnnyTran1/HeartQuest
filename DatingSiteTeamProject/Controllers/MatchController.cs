using DatingSiteTeamProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DatingSiteTeamProject.Controllers
{
    public class MatchController : Controller
    {
        public IActionResult MatchView()
        {
            string userIdCookie = Request.Cookies["UserId"];
            ViewBag.UserId = userIdCookie;
            if (userIdCookie == null)
            {
                return RedirectToAction("Registration_View", "Registration");
            }

            int userId = int.Parse(userIdCookie);
            string matchesApiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/MatchApi/GetMatches?id={userId}";

            List<AuthenicationModel> matches = DatingHttp.GetProfilesFromApi(matchesApiUrl);

            MatchViewModel viewModel = new MatchViewModel
            {
                ListOfMatches = matches
            };

            return View(viewModel);
        }

        public IActionResult Unmatch(int unmatcher, int personBeingUnmatched)
        {
            string userIdCookie = Request.Cookies["UserId"];
            ViewBag.UserId = userIdCookie;

            MatchModel match = new MatchModel(unmatcher, personBeingUnmatched);
            match.Unmatch();

            return RedirectToAction("ProfileHomeView", "Profile");
        }

        public IActionResult RequestDate(int senderId, int recieverId)
        {
            DateModel date = new DateModel(senderId, recieverId);

            string jsonLike = JsonSerializer.Serialize(date);

            string apiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/DateApi/RequestDate";

            DatingHttp.SendHttpRequest(apiUrl, "POST", jsonLike);

            return RedirectToAction("ProfileHomeView", "Profile");

        }
    }
}
