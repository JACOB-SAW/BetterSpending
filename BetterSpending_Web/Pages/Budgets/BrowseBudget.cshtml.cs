using BetterSpending_Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using BetterSpending_Web.Model;
using Microsoft.AspNetCore.Authorization;

namespace BetterSpending_Web.Pages.Budgets
{
    [Authorize] // Ensures that only authenticated users can access this page
    public class BrowseBudgetModel : PageModel
    {
        // List to store budget data retrieved from the database
        public List<BudgetView> Budget { get; set; } = new List<BudgetView>();

        // Handles GET requests to the page
        public void OnGet()
        {
            // Populate the budget list when the page is loaded
            PopulateBudgetList();
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

            // Refresh the budget list after deletion
            PopulateBudgetList();

            // Redirect to the same page to reflect the updated budget list
            return RedirectToPage();
        }

        // Populates the Budget list with data from the database
        private void PopulateBudgetList()
        {
            // Establish a connection to the database
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                // SQL query to fetch budget data, including related category and language details
                string cmdText = "SELECT b.BudgetsID, b.BudgetName, b.TotalAmount, b.UserID, b.CategoryID, b.RecurringID, l.LanguageName, " +
                                 "c.CategoryName, c.CategoryType " +
                                 "FROM Budgets b " +
                                 "JOIN Categories c ON b.CategoryID = c.CategoryID " +
                                 "JOIN SystemUser u ON b.UserID = u.UserID " +
                                 "JOIN Language l ON b.LanguageID = l.LanguageID";

                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(); // Execute the query and retrieve the data

                // Check if the query returned any rows
                if (reader.HasRows)
                {
                    // Iterate through the result set and populate the Budget list
                    while (reader.Read())
                    {
                        BudgetView budget = new BudgetView
                        {
                            BudgetsID = Convert.ToInt32(reader["BudgetsID"]), // Budget ID
                            BudgetName = reader["BudgetName"].ToString(), // Budget name
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]), // Total amount
                            UserID = Convert.ToInt32(reader["UserID"]), // User ID
                            CategoryID = Convert.ToInt32(reader["CategoryID"]), // Category ID
                            RecurringID = reader["RecurringID"] as DateTime?, // Nullable recurring date
                            LanguageName = reader["LanguageName"].ToString(), // Language name
                            CategoryName = reader["CategoryName"].ToString(), // Category name
                            CategoryType = reader["CategoryType"].ToString() // Category type
                        };

                        // Add the budget to the list
                        Budget.Add(budget);
                    }
                }
            }
        }
    }
}
