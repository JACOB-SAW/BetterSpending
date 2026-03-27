using BetterSpending_Business;
using BetterSpending_Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BetterSpending_Web.Pages.Account
{
    [Authorize(Roles = "Creator")]
    public class ViewUsersModel : PageModel
    {
        public List<Users> User { get; set; } = new List<Users>();
        public void OnGet()
        {
            populateUserList();
        }
        public IActionResult OnPostDelete(int id)
        {
            //establish connection to the database
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                //deletes the user with the specified id
                string cmdText = "DELETE FROM [SystemUser] WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@UserID", id);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    ModelState.AddModelError(string.Empty, "No User found with the specified ID.");
                    populateUserList();
                    return Page(); // Return to the same page with the error message
                }
            }
            populateUserList(); // Refresh the budget list after deletion
            return RedirectToPage("/Account/ViewUsers"); // Redirect to the same page to refresh the budget list
        }
        
        public void populateUserList()
        {
            try
            {
                //establish connection to the database
                using (SqlConnection connection = new SqlConnection(AppHelper.GetConnectionString()))
                {
                    //selects UserID, SystemUserFirstName, SystemUserLastName, SystemUserEmail, SystemUserProfileImage, AccountTypeID, AccountTypeName from the database
                    connection.Open();
                    string cmdText = "SELECT " +
                        "SystemUser.UserID, " +
                        "SystemUser.SystemUserFirstName, " +
                        "SystemUser.SystemUserLastName, " +
                        "SystemUser.SystemUserEmail, " +
                        "SystemUser.SystemUserProfileImage, " +
                        "SystemUser.AccountTypeID, " +
                        "AccountType.AccountTypeName, " +
                        "SystemUser.LastLogin " +
                        "FROM [SystemUser] " +
                        "INNER JOIN AccountType ON SystemUser.AccountTypeID = AccountType.AccountTypeID " + // Add space here
                        "WHERE SystemUser.AccountTypeID = 2"; // Add space before WHERE


                    using (SqlCommand cmd = new SqlCommand(cmdText, connection)) //creates connection
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read()) // Ensure we iterate through the reader
                            {
                                // Populate the User list with data from the reader
                                User.Add(new Users
                                {
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    FirstName = reader.GetString(reader.GetOrdinal("SystemUserFirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("SystemUserLastName")),
                                    Email = reader.GetString(reader.GetOrdinal("SystemUserEmail")),
                                    AccountType = reader.GetInt32(reader.GetOrdinal("AccountTypeID")),
                                    AccountTypeName = reader.GetString(reader.GetOrdinal("AccountTypeName")),
                                });
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "No users found.");
                        }
                    }
                }
            }
            catch
            {
                // Handle exception (e.g., log it)
                throw;
            }
        }
    }
}
