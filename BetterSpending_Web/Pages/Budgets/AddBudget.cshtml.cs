using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using BetterSpending_Web.Model;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using BetterSpending_Business;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BetterSpending_Web.Pages.Budgets
{
    [Authorize] // Ensures that only authenticated users can access this page
    public class AddBudgetModel : PageModel
    {
        // The budget model bound to the form input
        [BindProperty]
        public BudgetModel NewBudget { get; set; } // Fixed CS0118 and IDE1006

        // Dropdown list for categories
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>(); // Initialized to avoid CS8618

        // Dropdown list for languages
        public List<SelectListItem> Language { get; set; } = new List<SelectListItem>(); // Initialized to avoid CS8618

        // List of selected budget IDs (not used in this example but initialized to avoid CS8618)
        public List<int> SelectedBudgetIDs { get; set; } = new List<int>();

        // Handles GET requests to initialize the page
        public void OnGet()
        {
            // Initialize a new budget model
            NewBudget = new BudgetModel();

            // Populate dropdown lists for categories and languages
            PopulateLanguageList();
            PopulateCategoryList();
        }

        // Handles POST requests to save the budget to the database
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                PopulateLanguageList();
                PopulateCategoryList();
                return Page();
            }

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to determine the logged-in user.");
                PopulateLanguageList();
                PopulateCategoryList();
                return Page();
            }

            NewBudget.UserID = int.Parse(userIdClaim.Value);

            int newBudgetId;
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                // inserts the new budget into the database
                string cmdText = @"
            INSERT INTO [Budgets] (BudgetName, TotalAmount, UserID, CategoryID, RecurringID, LanguageID) 
            OUTPUT INSERTED.BudgetsID
            VALUES (@BudgetName, @TotalAmount, @UserID, @CategoryID, @RecurringID, @LanguageID)";
                SqlCommand cmd = new SqlCommand(cmdText, conn);

                //sets the parameters for the SQL command
                cmd.Parameters.AddWithValue("@BudgetName", NewBudget.BudgetName);
                cmd.Parameters.AddWithValue("@TotalAmount", NewBudget.TotalAmount);
                cmd.Parameters.AddWithValue("@UserID", NewBudget.UserID);
                cmd.Parameters.AddWithValue("@CategoryID", NewBudget.CategoryID);
                cmd.Parameters.AddWithValue("@RecurringID", (object?)NewBudget.RecurringID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LanguageID", NewBudget.LanguageID);

                conn.Open();
                newBudgetId = (int)cmd.ExecuteScalar(); // Retrieve the ID of the newly inserted budget
            }

            // Redirect to the BudgetView page with the new budget ID
            return RedirectToPage("/Budgets/BudgetView", new { id = newBudgetId });
        }


        // Populates the Language dropdown list with data from the database
        private void PopulateLanguageList()
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                // SQL query to fetch language data
                string query = "SELECT LanguageID, LanguageName FROM Language";

                SqlCommand cm = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cm.ExecuteReader();

                // Read the data and populate the Language list
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var language = new SelectListItem
                        {
                            Value = reader["LanguageID"].ToString(),
                            Text = reader["LanguageName"].ToString()
                        };
                        Language.Add(language);
                    }
                }
            }
        }

        // Populates the Category dropdown list with data from the database
        private void PopulateCategoryList()
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                // SQL query to fetch category data
                string query = "SELECT CategoryID, CategoryName FROM Categories";

                SqlCommand cm = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cm.ExecuteReader();

                // Read the data and populate the Categories list
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var category = new SelectListItem
                        {
                            Value = reader["CategoryID"].ToString(),
                            Text = reader["CategoryName"].ToString()
                        };
                        Categories.Add(category);
                    }
                }
            }
        }
    }
}