using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BetterSpending_Web.Model;
using Microsoft.Data.SqlClient;
using BetterSpending_Business;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BetterSpending_Web.Pages.Account
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public Login newLogin { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(string? ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                // Validate user input

                // Check if user exists in the database
                // If user exists, redirect to profile page
                // If user does not exist, return error message
                using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
                {
                    string cmdText = "SELECT UserID, SystemUserPassword, AccountType.AccountTypeName " +
                        "FROM [SystemUser] " +
                        "INNER JOIN [AccountType] ON [SystemUser].AccountTypeID = AccountType.AccountTypeID " +
                        "WHERE SystemUserEmail = @Email";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@Email", newLogin.Email);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // User exists, redirect to profile page
                        reader.Read();
                        string passwordHash = reader.GetString(1);
                        if (AppHelper.ValidatePassword(newLogin.Password, passwordHash))
                        {

                            // Set email claim
                            // Set email claim
                            Claim emailClaim = new Claim(ClaimTypes.Email, newLogin.Email);

                            // userID claim
                            Claim userIDClaim = new Claim(ClaimTypes.NameIdentifier, reader.GetInt32(0).ToString());

                            // name claim
                            Claim nameClaim = new Claim(ClaimTypes.Name, reader.GetString(2));

                            //Role Claim
                            Claim roleClaim = new Claim(ClaimTypes.Role, reader.GetString(2));

                            // Create a list of claims
                            List<Claim> claims = new List<Claim> { emailClaim, userIDClaim, nameClaim, roleClaim };

                            // Claim identity
                            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                           


                            // Claims principal
                            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                            // Sign in the user
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                            // set the last login date
                            UpdateUserLoginTime(reader.GetInt32(0));

                            //redirect to the return url or profile page
                            if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                            {
                                return LocalRedirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToPage("/Account/Profile");
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("LoginError", "Invalid Credentials");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("LoginError", "Invalid Credentials");
                        return Page();
                    }
                }
            }
            else
            {
                return Page();
            }
        }
        //updates the users last login time
        private void UpdateUserLoginTime(int v)
        {
            using(SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                string cmdText = "UPDATE [SystemUser] SET LastLogin = @LastLogin WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@LastLogin", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", v);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
