using Microsoft.AspNetCore.Mvc.RazorPages;
using BetterSpending_Web.Model;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using BetterSpending_Business;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BetterSpending_Web.Pages.Account
{
     // Ensures that only authenticated users can access this page
    public class ProfileModel : PageModel
    {
        // The user profile model bound to the page
        [BindProperty]
        public UserProfile UserProfile { get; set; } = new UserProfile();

        // Handles GET requests to load the user's profile
        public void OnGet()
        {
            // Retrieve the logged-in user's ID from the authentication context
            int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Populate the UserProfile object with data from the database
            PopulateUserProfile(userID);
        }
        //
        // Populates the UserProfile object with data for the specified user
        private void PopulateUserProfile(int UserID)
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                // SQL query to fetch user profile data, including account type and last login
                string cmdText = "SELECT SystemUserFirstName, SystemUserLastName, SystemUserEmail, SystemUserProfileImage, AccountTypeName, LastLogin " +
                                 "FROM [SystemUser] INNER JOIN AccountType " +
                                 "ON [SystemUser].AccountTypeID = AccountType.AccountTypeID WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@UserID", UserID); // Add the UserID parameter to the query

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // If the user exists, populate the UserProfile object with the retrieved data
                if (reader.HasRows)
                {
                    reader.Read();
                    UserProfile.FirstName = reader.GetString(0);
                    UserProfile.LastName = reader.GetString(1);
                    UserProfile.Email = reader.GetString(2);
                    UserProfile.ProfileImageURL = reader.GetString(3);
                    UserProfile.AccountType = reader.GetString(4);
                    UserProfile.LastLogin = reader.GetDateTime(5);
                }
            }
        }
    }
}