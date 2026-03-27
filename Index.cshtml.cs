using BetterSpending_Business;
using BetterSpending_Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BetterSpending_Web.Pages.Categories
{
    [Authorize(Roles = "Creator")] // Restricts access to users with the "Creator" role
    public class CategoryListModel : PageModel
    {
        // List to store the categories retrieved from the database
        public List<CategoryModel> CatList { get; set; } = new List<CategoryModel>();

        // Handles GET requests to the page
        public void OnGet()
        {
            // Populate the category list when the page is loaded
            populateCategoryList();
        }

        // Populates the CatList property with categories from the database
        public void populateCategoryList()
        {
            try
            {
                // Establish a connection to the database
                using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
                {
                    conn.Open(); // Open the database connection

                    // SQL query to fetch all categories, ordered by name
                    string sql = "SELECT CategoryID, CategoryName, CategoryType FROM Categories ORDER BY CategoryName";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader()) // Execute the query and retrieve the data
                        {
                            // Iterate through the result set and populate the CatList
                            while (reader.Read())
                            {
                                CatList.Add(new CategoryModel
                                {
                                    CategoryID = reader.GetInt32(0), // Category ID
                                    CategoryName = reader.GetString(1), // Category name
                                    CategoryType = reader.GetString(2) // Category type
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                // Handle any exceptions that occur during the database operation
                // (Optional: Log the exception for debugging purposes)
                throw;
            }
        }

        // Handles POST requests to delete a category by its ID
        public IActionResult OnPostDelete(int id)
        {
            // Establish a connection to the database
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                // SQL command to delete a category with the specified ID
                string cmdText = "DELETE FROM Categories WHERE CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@CategoryID", id); // Add the category ID as a parameter
                conn.Open(); // Open the database connection

                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the delete command

                // If no rows were affected, it means the category ID was not found
                if (rowsAffected == 0)
                {
                    ModelState.AddModelError(string.Empty, "No category found with the specified ID.");
                }
            }

            // Refresh the category list after deletion
            populateCategoryList();

            // Redirect to the same page to reflect the updated category list
            return RedirectToPage();
        }
    }
}
