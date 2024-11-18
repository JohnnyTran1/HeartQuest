namespace DatingSiteApi.Models
{
    public class SearchViewModel
    {
        public string Term { get; set; }
        public string Location { get; set; }

        //public int Limit { get; set; }


        public Yelp.Api.Models.SearchResponse Response { get; set; }
        //public bool SearchAttempted { get; set; } = false;

    }
}
