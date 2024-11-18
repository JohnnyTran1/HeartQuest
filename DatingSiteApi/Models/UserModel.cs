using System.ComponentModel.DataAnnotations;

namespace DatingSiteApi.Models
{
    public class UserModel
    {
        //need name
        private string name;
        private string email;
        private string username;
        private string password;
        private int id;
        //not sure if need id field
        //might need to add cookies field here? or create cookies class? not sure yet.

        //constructor for userModel
        public UserModel()
        {
            this.name = string.Empty;
            this.email = string.Empty;
            this.username = string.Empty;
            this.password = string.Empty;
            this.id = 0;
        }


        public UserModel(string name, string email, string userName, string password, int id)
        {
            this.name = name;
            this.email = email;
            this.username = userName;
            this.password = password;
            this.id = id;
        }

        public UserModel(string name, string email, string userName, string password)
        {
            this.name = name;
            this.email = email;
            this.username = userName;
            this.password = password;
        }

        //for logging in
        public UserModel(string userName, string password)
        {
            this.username = userName;
            this.password = password;
        }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should only contain letters!")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Email cannot be longer than 50 characters.")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Username should only contain letters and numbers!")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        [Required(ErrorMessage = "Password is required.")]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        //need to learn how to add cookie here
    }
}
