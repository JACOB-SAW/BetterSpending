using BetterSpending_Business;
using BetterSpending_Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BetterSpending_Web.Pages.Categories
{
    [Authorize(Roles = "Creator")] // Restricts access to users with the "Creator" role
    public class EditCategoryModel : PageModel
    {
        [BindProperty]
        public CategoryModel EditCat { get; set; } = new(); // Holds the data for the category being edited

        // Handles GET requests to load the category data for editing
        public void OnGet(int id)
        {
            // Fetch the category data from the database based on the provided ID
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                string query = "SELECT CategoryID, CategoryName, CategoryType FROM Categories WHERE CategoryID = @CategoryID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", id); // Add the category ID as a parameter
                    conn.Open(); // Open the database connection
                    using (SqlDataReader reader = cmd.ExecuteReader()) // Execute the query and retrieve the data
                    {
                        if (reader.Read()) // If a record is found, populate the EditCat property
                        {
                            EditCat.CategoryID = reader.GetInt32(0); // Category ID
                            EditCat.CategoryName = reader.GetString(1); // Category name
                            EditCat.CategoryType = reader.GetString(2); // Category type
                        }
                    }
                }
            }
        }

        // Handles POST requests to update the category in the database
        public IActionResult OnPost(int id)
        {
            // Check if the model state is valid (e.g., all required fields are filled)
            if (!ModelState.IsValid)
            {
                return Page(); // Return to the same page with validation errors
            }

            // Update the category in the database
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                string query = "UPDATE Categories SET [CategoryName] = @CategoryName, [CategoryType] = @CategoryType WHERE [CategoryID] = @CategoryID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to the SQL command to prevent SQL injection
                    cmd.Parameters.AddWithValue("@CategoryName", EditCat.CategoryName); // Updated category name
                    cmd.Parameters.AddWithValue("@CategoryType", EditCat.CategoryType); // Updated category type
                    cmd.Parameters.AddWithValue("@CategoryID", id); // Category ID to update
                    conn.Open(); // Open the database connection
                    cmd.ExecuteNonQuery(); // Execute the update command
                }
            }

            // Redirect to the category list page after successfully updating the category
            return RedirectToPage("/Categories/CategoryList");
        }
    }
}
