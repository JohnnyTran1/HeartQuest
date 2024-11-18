using Utilities;
using System;
using System.Data;
using System.Data.SqlClient;
using DatingSiteTeamProject.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Components.Web;
using DatingSiteTeamProject.Helpers;

namespace DatingSiteTeamProject.Models
{
    public class LoginDBModel
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        public (int UserId, bool IsValid, string ErrorMessage) AuthenticateUser(string username, string password)
        {
            // objCommand.CommandText = "dbo.Login";


            SqlParameter paramUsername = new SqlParameter("@Username", username);
            //     SqlParameter paramPassword = new SqlParameter("@Password", user.Password);

            objCommand.Parameters.Add(paramUsername);
            //      objCommand.Parameters.Add(paramPassword);

            SqlParameter parameterId = new SqlParameter("@UserId", SqlDbType.Int);
            parameterId.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(parameterId);
            SqlParameter paramHashedPassword = new SqlParameter("@PassReturned", SqlDbType.VarChar, -1);
            paramHashedPassword.Direction = ParameterDirection.Output;

            objCommand.Parameters.Add(paramHashedPassword);

            try
            {

                    objDB.GetDataSet(objCommand);
                int userId = Convert.ToInt32(objCommand.Parameters["@UserId"].Value);
                string hashedPassword = Convert.ToString(objCommand.Parameters["@PassReturned"].Value);

                if (userId > 0 && !string.IsNullOrEmpty(hashedPassword) && Hasher.VerifyPassword(password, hashedPassword))
                {
                    return (userId, true, null);
                }
                return (0, false, "Invalid username or password.");
            }
            catch (Exception ex)
            {
                return (0, false, ex.Message);
            }
        }
    }
}
