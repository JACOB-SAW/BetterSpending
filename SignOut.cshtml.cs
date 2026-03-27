using Microsoft.AspNetCore.Mvc.RazorPages;
using BetterSpending_Web.Model;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using BetterSpending_Business;
using Microsoft.AspNetCore.SignalR;

namespace BetterSpending_Web.Pages.Account
{
    public class Register : PageModel
    {
        [BindProperty]
        public Registration newUser { get; set; } = new Registration();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Save to database

                using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
                {
                    //create command to insert data
                    string cmdText = "INSERT INTO [SystemUser] (SystemUserFirstName, SystemUserLastName, SystemUserProfileImage, SystemUserEmail, SystemUserPassword, AccountTypeID) VALUES (@FirstName, @LastName, @ProfileImage, @Email, @Password, 2)";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", newUser.LastName);
                    cmd.Parameters.AddWithValue("@ProfileImage", "default.jpg");
                    cmd.Parameters.AddWithValue("@Email", newUser.Email);
                    cmd.Parameters.AddWithValue("@Password", AppHelper.GeneratePasswordHash(newUser.Password));
                    //execute the command
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }

                // Redirect to login page
                return RedirectToPage("/Account/SignIn");
            }
            else
            {
                return Page();
            }

        }
    }
}