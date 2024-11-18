using System.ComponentModel.DataAnnotations;

namespace DatingSiteApi.Models
{
    public class AuthenicationModel
    {
        private UserModel user;
        private ProfileModel profile;
        private string answer;
        private string fourDigitCode;
        private ImageInfo image;
        private SearchViewModel searchViewModel;
        private DateModel dateModel;

        public AuthenicationModel()
        {
            this.user = new UserModel();
            this.profile = new ProfileModel();
            this.fourDigitCode = "";
            this.searchViewModel = new SearchViewModel();
            this.dateModel = new DateModel();
        }

        public DateModel DateModel
        {
            get { return this.dateModel; }
            set { this.dateModel = value; }
        }

        public SearchViewModel SearchViewModel
        {
            get { return this.searchViewModel; }
            set { this.searchViewModel = value; }
        }

        public ImageInfo Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        public UserModel User
        {
            get { return this.user; }
            set { this.user = value; }
        }

        public ProfileModel Profile
        {
            get { return this.profile; }
            set { this.profile = value; }
        }

        //public List<string> GetStates()
        //{
        //   return ProfileDBModel.GetStates();
        //}
        [Required(ErrorMessage = "Answer is required.")]
        [StringLength(50, ErrorMessage = "Answer cannot be longer than 50 characters.")]
        public string Answer
        {
            get { return this.answer; }
            set { this.answer = value; }
        }

        //[Required(ErrorMessage = "Digital code is required.")]
        public string FourDigitCode
        {
            get { return this.fourDigitCode; }
            set { this.fourDigitCode = value; }
        }
    }
}
