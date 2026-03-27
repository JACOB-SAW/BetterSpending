using BetterSpending_Business;
using BetterSpending_Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BetterSpending_Web.Pages.Budgets
{
    [Authorize] // Ensures that only authenticated users can access this page
    public class BudgetViewModel : PageModel
    {
        // Property to hold the budget details for the current view
        public BudgetView Budget { get; set; } = new BudgetView();

        // Handles GET requests to the page and populates budget information based on the provided ID
        public void OnGet(int id)
        {
            populateBudgetInfo(id); // Fetch and populate budget details
        }

        // Handles POST requests to delete a budget by its ID
        public IActionResult OnPostDelete(int id)
        {
            // Establish a connection to the database
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                // SQL command to delete a budget with the specified ID
                string cmdText = "DELETE FROM Budgets WHERE BudgetsID = @BudgetsID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@BudgetsID", id); // Add the budget ID as a parameter
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the delete command

                // If no rows were affected, it means the budget ID was not found
                if (rowsAffected == 0)
                {
                    ModelState.AddModelError(string.Empty, "No budget found with the specified ID.");
                }
            }

            // Refresh the budget information after deletion
            populateBudgetInfo(id);

            // Redirect to the home page after deletion
            return RedirectToPage("/Index");
        }

        // Populates the Budget property with information for the specified budget ID
        public void populateBudgetInfo(int id)
        {
            try
            {
                // SQL query to fetch budget details, including related category and language information
                string cmdText = "SELECT b.BudgetsID, b.BudgetName, b.TotalAmount, b.UserID, b.CategoryID, b.RecurringID, l.LanguageName, " +
                                 "c.CategoryName, c.CategoryType " +
                                 "FROM Budgets b " +
                                 "JOIN Categories c ON b.CategoryID = c.CategoryID " +
                                 "JOIN SystemUser u ON b.UserID = u.UserID " +
                                 "JOIN Language l ON b.LanguageID = l.LanguageID " +
                                 "WHERE b.BudgetsID = @BudgetsID";

                // Establish a connection to the database
                using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@BudgetsID", id); // Add the budget ID as a parameter
                    SqlDataReader reader = cmd.ExecuteReader(); // Execute the query and retrieve the data

                    // Check if the query returned any rows
                    if (reader.HasRows)
                    {
                        // Iterate through the result set and populate the Budget property
                        while (reader.Read())
                        {
                            Budget = new BudgetView
                            {
                                BudgetsID = Convert.ToInt32(reader["BudgetsID"]), // Budget ID
                                BudgetName = reader["BudgetName"]?.ToString() ?? string.Empty, // Budget name (handle null)
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]), // Total amount
                                UserID = Convert.ToInt32(reader["UserID"]), // User ID
                                CategoryID = Convert.ToInt32(reader["CategoryID"]), // Category ID
                                RecurringID = reader["RecurringID"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["RecurringID"]), // Nullable recurring date
                                LanguageName = reader["LanguageName"]?.ToString() ?? string.Empty, // Language name (handle null)
                                CategoryName = reader["CategoryName"]?.ToString() ?? string.Empty, // Category name (handle null)
                                CategoryType = reader["CategoryType"]?.ToString() ?? string.Empty  // Category type (handle null)
                            };
                        }
                    }
                    else
                    {
                        // Add a model error if no budget is found with the specified ID
                        ModelState.AddModelError(string.Empty, "No budget found with the specified ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is implemented) and rethrow it with additional context
                throw new Exception("An error occurred while retrieving budget information.", ex);
            }
        }
    }
}
