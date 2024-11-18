namespace DatingSiteTeamProject.Models
{
    public class DateViewModel
    {
        public List<AuthenicationModel> ListOfAcceptedRequests { get; set; }

        public List<AuthenicationModel> ListOfIncomingDateRequests { get; set; }

        public List<AuthenicationModel> ListOfSentRequests { get; set; }

        public SearchViewModel Search { get; set; }


    }
}