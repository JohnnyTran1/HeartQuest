using DatingSiteApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Utilities;
using System.Data;
using System.Data.Common;

namespace DatingSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesApiController : Controller
    {

        [HttpPost("LikeUser")]
        public Boolean LikeUser([FromBody] LikeModel like) 
        {
            int result = like.LikeUser();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete("UnlikeUser")]
        public Boolean UnlikeUser([FromBody] LikeModel like)
        {
            int result = like.Unlike();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet("GetProfilesThatLikeCurrentUser")]
        public List<AuthenicationModel> GetProfilesThatLikeCurrentUser(int id)
        {
            List<AuthenicationModel> listOfProfiles = new List<AuthenicationModel>();
            LikeModel like = new LikeModel();
            like.LikerId = id;

            DataSet profilesDataSet = like.GetLikedYou();


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
                    DreamDestinations = row["DreamDestinations"].ToString(),
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

        [HttpGet("GetProfilesThatCurrentUserLikes")]
        public List<AuthenicationModel> GetProfilesThatCurrentUserLikes(int id)
        {
            List<AuthenicationModel> listOfProfiles = new List<AuthenicationModel>();
            LikeModel like = new LikeModel();
            like.LikerId = id;

            DataSet profilesDataSet = like.GetLikedByUser();

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



    }
}
