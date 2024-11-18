using System.Text;

namespace DatingSiteTeamProject.Helpers
{
    public class RandomCode
    {
        public static string VerificationCode()
        {
            Random random = new Random();

            int random4DigitNumber = random.Next(1000, 9999);
           
            return random4DigitNumber.ToString();
        }
    }
}
