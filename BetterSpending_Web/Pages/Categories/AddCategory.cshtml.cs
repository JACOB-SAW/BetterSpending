using BetterSpending_Business;
using BetterSpending_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace BetterSpending_Web.Pages.Categories
{
    [Authorize(Roles = "Creator")] // Restricts access to users with the "Creator" role
    public class AddCategoryModel : PageModel
    {
        [BindProperty]
        public CategoryModel NewCat { get; set; } = new CategoryModel(); // Holds the data for the new category being added

        // Handles GET requests to the page
        public void OnGet()
        {
            // No specific logic is needed for GET requests in this case
        }

        // Handles POST requests to add a new category
        public IActionResult OnPost()
        {
            // Check if the model state is valid (e.g., all required fields are filled)
            if (ModelState.IsValid)
            {
                try
                {
                    // Establish a connection to the database
                    using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
                    {
                        // SQL command to insert a new category into the database
                        string cmdText = "INSERT INTO [Categories] (CategoryName, CategoryType) " +
                                         "VALUES (@CategoryName, @CategoryType)";
                        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                        {
                            // Add parameters to the SQL command to prevent SQL injection
                            cmd.Parameters.AddWithValue("@CategoryName", NewCat.CategoryName);
                            cmd.Parameters.AddWithValue("@CategoryType", NewCat.CategoryType);
                            conn.Open(); // Open the database connection
                            cmd.ExecuteNonQuery(); // Execute the SQL command
                        }
                    }

                    // Redirect to the category list page after successfully adding the category
                    return RedirectToPage("/Categories/CategoryList");
                }
                catch
                {
                    // Handle any exceptions that occur during the database operation
                    // (Optional: Log the exception for debugging purposes)
                    throw;
                }
            }

            // If the model state is invalid, redisplay the form with validation errors
            return Page();
        }
    }
}
