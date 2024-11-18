using System.Threading.Tasks;
using Yelp.Api;
using Yelp.Api.Models;

namespace DatingSiteTeamProject.Helpers
{
    public class YelpService
    {
        private const string ApiKey = "";
        private readonly Client _client;

        public YelpService()
        {
            _client = new Client(ApiKey);
        }

        public async Task<SearchResponse> SearchBusinessesAsync(string term, string location)
        {
            var request = new SearchRequest
            {
                Term = term,
                Location = location
            };
            try
            {
                var response = await _client.SearchBusinessesAllAsync(request);
                response.Businesses = response.Businesses.Take(5).ToList();
                return response;
            }
            catch (Exception ex)
            {
                // Properly log or handle exceptions
                throw new ApplicationException("Error accessing Yelp API", ex);
            }
        }


    }
}