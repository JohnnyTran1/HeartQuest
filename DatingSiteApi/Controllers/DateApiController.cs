using DatingSiteApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Utilities;
using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Numerics;

namespace DatingSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateApiController : Controller
    {

        [HttpPost("RequestDate")]
        public Boolean RequestDate([FromBody] DateModel date)
        {
            int result = date.RequestDate();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut("RespondToDateRequest")]
        public Boolean RespondToDateRequest([FromBody] DateModel date)
        {
            int result = date.RespondToDateRequest();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut("ModifyDate")]
        public Boolean ModifyDate([FromBody] DateModel date)
        {
            int result = date.ModifyDate();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        [HttpGet("GetListOfIncomingDateRequests")]
        public List<AuthenicationModel> GetListOfIncomingDateRequests(int id)
        {
            List<AuthenicationModel> listOfProfiles = new List<AuthenicationModel>();
            DateModel date = new DateModel();
            date.SenderId = id;

            DataSet profilesDataSet = date.GetListOfIncomingDateRequests();

            foreach (DataRow row in profilesDataSet.Tables[0].Rows)
            {

                ProfileModel profileModel = new ProfileModel
                {
                    Age = row["Age"].ToString(),
                    City = row["City"].ToString(),
                    State = row["State"].ToString(),
                    PhotoUrl = row["PhotoUrl"].ToString(),
                    Commitment = row["Commitment"].ToString(),
                    Description = row["Description"].ToString(),
                    Occupation = row["Occupation"].ToString(),
                    Height = Math.Round(Convert.ToDouble(row["Height"]), 2).ToString(),
                    Weight = row["Weight"].ToString(),
                };

                UserModel userModel = new UserModel
                {
                    Name = row["Fullname"].ToString(),
                    Username = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    Id = (int)Convert.ToInt64(row["Id"]),
                };

                AuthenicationModel authenticatedProfile = new AuthenicationModel
                {
                    Profile = profileModel,
                    User = userModel
                };

                listOfProfiles.Add(authenticatedProfile);
            }
            return listOfProfiles;
        }

        [HttpGet("GetListOfAcceptedRequests")]
        public List<AuthenicationModel> GetListOfAcceptedRequests(int id)
        {
            List<AuthenicationModel> listOfProfiles = new List<AuthenicationModel>();
            DateModel date = new DateModel();
            date.SenderId = id;

            DataSet profilesDataSet = date.GetListOfAcceptedRequests();

            foreach (DataRow row in profilesDataSet.Tables[0].Rows)
            {

                ProfileModel profileModel = new ProfileModel
                {
                    Age = row["Age"].ToString(),
                    City = row["City"].ToString(),
                    State = row["State"].ToString(),
                    PhotoUrl = row["PhotoUrl"].ToString(),
                    Commitment = row["Commitment"].ToString(),
                    Occupation = row["Occupation"].ToString(),
                    Description = row["Description"].ToString(),
                    Height = Math.Round(Convert.ToDouble(row["Height"]), 2).ToString(),
                    Weight = row["Weight"].ToString(),

                };

                UserModel userModel = new UserModel
                {
                    Name = row["Fullname"].ToString(),
                    Username = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    Id = (int)Convert.ToInt64(row["Id"]),
                };

                DateModel dateModel = new DateModel
                {
                    Location = row["Location"].ToString(),
                    DateDescription = row["DateDescription"].ToString(),
                    TimeOfDate = row["TimeOfDate"].ToString(),
                    DayAndMonthOfDate = row["DayNMonth"].ToString(),
                };

                AuthenicationModel authenticatedProfile = new AuthenicationModel
                {
                    Profile = profileModel,
                    User = userModel,
                    DateModel = dateModel
                };

                listOfProfiles.Add(authenticatedProfile);
            }
            return listOfProfiles;
        }

        [HttpGet("GetListOfSentRequests")]
        public List<AuthenicationModel> GetListOfSentRequests(int id)
        {
            List<AuthenicationModel> listOfProfiles = new List<AuthenicationModel>();
            DateModel date = new DateModel();
            date.SenderId = id;

            DataSet profilesDataSet = date.GetListOfSentRequests();

            foreach (DataRow row in profilesDataSet.Tables[0].Rows)
            {

                ProfileModel profileModel = new ProfileModel
                {
                    Age = row["Age"].ToString(),
                    City = row["City"].ToString(),
                    State = row["State"].ToString(),
                    PhotoUrl = row["PhotoUrl"].ToString(),
                    Commitment = row["Commitment"].ToString(),
                    Description = row["Description"].ToString(),
                };

                UserModel userModel = new UserModel
                {
                    Name = row["Fullname"].ToString(),
                    Username = row["Username"].ToString(),
                    Id = (int)Convert.ToInt64(row["Id"]),
                };

                AuthenicationModel authenticatedProfile = new AuthenicationModel
                {
                    Profile = profileModel,
                    User = userModel
                };

                listOfProfiles.Add(authenticatedProfile);
            }
            return listOfProfiles;
        }


        //<!-- ACCEPT DATE HTTP PUT -->
        //<!-- Modify DATE HTTP PUT -->
        // <!-- DENY Date button HTTP DELETE -->




    }
}
