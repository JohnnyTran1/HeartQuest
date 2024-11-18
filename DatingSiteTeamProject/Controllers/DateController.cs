using DatingSiteTeamProject.Helpers;
using DatingSiteTeamProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DatingSiteTeamProject.Controllers
{
    public class DateController : Controller
    {
        YelpService yelpService = new YelpService();
        //DateViewModel viewModel = new DateViewModel();
        public IActionResult DateView()
        {
            string userIdCookie = Request.Cookies["UserId"];
            ViewBag.UserId = userIdCookie;
            if (userIdCookie == null)
            {
                return RedirectToAction("Registration_View", "Registration");
            }

            int userId = int.Parse(userIdCookie);
            string listOfIncomingDateRequestsApiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/DateApi/GetListOfIncomingDateRequests?id={userId}";
            string listOfAcceptedRequestsApiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/DateApi/GetListOfAcceptedRequests?id={userId}";
            string listOfSentRequestsApiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/DateApi/GetListOfSentRequests?id={userId}";

            List<AuthenicationModel> listOfIncomingDateRequests = DatingHttp.GetProfilesFromApi(listOfIncomingDateRequestsApiUrl);
            List<AuthenicationModel> listOfAcceptedRequests = DatingHttp.GetProfilesFromApi(listOfAcceptedRequestsApiUrl);
            List<AuthenicationModel> listOfSentRequests = DatingHttp.GetProfilesFromApi(listOfSentRequestsApiUrl);

            DateViewModel viewModel = new DateViewModel
            {
                ListOfIncomingDateRequests = listOfIncomingDateRequests,
                ListOfAcceptedRequests = listOfAcceptedRequests,
                ListOfSentRequests = listOfSentRequests,
                Search = new SearchViewModel()

            };

            return View(viewModel);
        }

        public IActionResult RespondToDateRequest(int senderId, int recieverId, string response)
        {

            DateModel date = new DateModel(senderId, recieverId, response);

            string jsonDate = JsonSerializer.Serialize(date);

            string apiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/DateApi/RespondToDateRequest";

            DatingHttp.SendHttpRequest(apiUrl, "PUT", jsonDate);

            return RedirectToAction("ProfileHomeView", "Profile");

        }


        public IActionResult ModifyDateRequest(int senderId, int recieverId, DateTime dayAndMonthOfDate, TimeSpan time, string dateDescription, string location)
        {
            string dateAsString = dayAndMonthOfDate.ToString("yyyy-MM-dd");
            string timeAsString = time.ToString(@"hh\:mm");
            DateModel date = new DateModel(senderId, recieverId, dateAsString, timeAsString, dateDescription, location);
            string jsonDate = JsonSerializer.Serialize(date);

            string apiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/DateApi/ModifyDate";

            DatingHttp.SendHttpRequest(apiUrl, "PUT", jsonDate);

            return RedirectToAction("ProfileHomeView", "Profile");

        }


        [HttpGet]
        public async Task<IActionResult> SearchYelp(string term, string location, int profileId)
        {
            string userIdCookie = Request.Cookies["UserId"];
            ViewBag.UserId = userIdCookie;
            string i = userIdCookie;

            if (userIdCookie == null)
            {
                return RedirectToAction("Registration_View", "Registration");
            }

            int userId = int.Parse(userIdCookie);
            string listOfIncomingDateRequestsApiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/DateApi/GetListOfIncomingDateRequests?id={userId}";
            string listOfAcceptedRequestsApiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/DateApi/GetListOfAcceptedRequests?id={userId}";
            string listOfSentRequestsApiUrl = $"https://cis-iis2.temple.edu/Spring2024/CIS3342_TUG41350/WebApi/api/DateApi/GetListOfSentRequests?id={userId}";

            List<AuthenicationModel> listOfIncomingDateRequests = DatingHttp.GetProfilesFromApi(listOfIncomingDateRequestsApiUrl);
            List<AuthenicationModel> listOfAcceptedRequests = DatingHttp.GetProfilesFromApi(listOfAcceptedRequestsApiUrl);
            List<AuthenicationModel> listOfSentRequests = DatingHttp.GetProfilesFromApi(listOfSentRequestsApiUrl);

            //yelp api
            var searchResults = new SearchViewModel(); // Initialize an empty model

            if (!string.IsNullOrWhiteSpace(term) && !string.IsNullOrWhiteSpace(location))
            {
                var results = await yelpService.SearchBusinessesAsync(term, location);

                //TempData["SearchPerformed"] = true; 
                //TempData["HasResults"] = response.Businesses != null && response.Businesses.Any()

                searchResults.Term = term;
                searchResults.Location = location;
                searchResults.Response = results;
                //searchResults.SearchAttempted = true;
                TempData["SearchPerformed"] = true;

            }


            DateViewModel viewModel = new DateViewModel
            {
                ListOfIncomingDateRequests = listOfIncomingDateRequests,
                ListOfAcceptedRequests = listOfAcceptedRequests,
                ListOfSentRequests = listOfSentRequests,
                Search = searchResults
            };
            var profile = viewModel.ListOfAcceptedRequests.FirstOrDefault(u => u.User.Id == profileId);

            profile.SearchViewModel = searchResults;

            return View("DateView", viewModel);
        }

        [HttpPost]
        public IActionResult SelectBusinessForDate(string BusinessName, int profileId, string BusinessAddress, string BusinessCity, string BusinessState, string BusinessZipCode)
        {
            int id = profileId;
            TempData["SelectedLocation"] = "Address: " + BusinessAddress + ", " + BusinessCity + ", " + BusinessState + " " + BusinessZipCode;
            TempData["Description"] = "Description: " + BusinessName;
            return RedirectToAction("DateView", new { profileId = profileId });
        }


    }
}