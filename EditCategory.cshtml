using BetterSpending_Business;
using BetterSpending_Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace BetterSpending_Web.Pages.Budgets
{
    [Authorize] // Ensures that only authenticated users can access this page
    public class EditBudgetModel : PageModel
    {
        [BindProperty]
        public EditBudget EditBudget { get; set; } = new(); // Holds the budget data being edited

        // Dropdown list for categories
        public List<SelectListItem> Categories { get; set; } = new();

        // Dropdown list for languages
        public List<SelectListItem> Language { get; set; } = new();

        // Handles GET requests to load the page and populate the budget data
        public void OnGet(int id)
        {
            PopulateLanguageList(); // Populate the language dropdown
            PopulateCategoryList(); // Populate the category dropdown
            LoadBudget(id); // Load the budget data for the given ID
        }

        // Handles POST requests to update the budget
        public IActionResult OnPost()
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                PopulateLanguageList(); // Repopulate the language dropdown
                PopulateCategoryList(); // Repopulate the category dropdown
                return Page(); // Return to the same page with validation errors
            }

            // Retrieve the logged-in user's ID from the claims
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            // If the user ID claim is not found, return an error
            if (userIdClaim == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to determine the logged-in user.");
                PopulateLanguageList();
                PopulateCategoryList();
                return Page();
            }

            // Set the UserID property of the budget to the logged-in user's ID
            EditBudget.UserID = int.Parse(userIdClaim.Value);

            // Update the budget in the database
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                string cmdText = @"
        UPDATE [Budgets] SET 
            [BudgetName] = @BudgetName,
            [TotalAmount] = @TotalAmount,
            [UserID] = @UserID,
            [CategoryID] = @CategoryID,
            [RecurringID] = @RecurringID,
            [LanguageID] = @LanguageID
        WHERE [BudgetsID] = @BudgetsID";

                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@BudgetName", EditBudget.BudgetName);
                cmd.Parameters.AddWithValue("@TotalAmount", EditBudget.TotalAmount);
                cmd.Parameters.AddWithValue("@UserID", EditBudget.UserID);
                cmd.Parameters.AddWithValue("@CategoryID", EditBudget.CategoryID);
                cmd.Parameters.AddWithValue("@RecurringID", (object?)EditBudget.RecurringID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LanguageID", EditBudget.LanguageID);
                cmd.Parameters.AddWithValue("@BudgetsID", EditBudget.BudgetsID);

                conn.Open();
                cmd.ExecuteNonQuery(); // Execute the update command
            }

            // Redirect to the BudgetView page with the updated budget ID
            return RedirectToPage("/Budgets/BudgetView", new { id = EditBudget.BudgetsID });
        }

        // Loads the budget data for the given ID from the database
        private void LoadBudget(int id)
        {
            using SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString());
            string query = "SELECT * FROM [Budgets] WHERE [BudgetsID] = @BudgetsID"; // Query to fetch budget data
            SqlCommand cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@BudgetsID", id); // Add the budget ID as a parameter
            conn.Open();
            SqlDataReader reader = cm.ExecuteReader();

            // If a record is found, populate the EditBudget property
            if (reader.Read())
            {
                EditBudget = new EditBudget
                {
                    BudgetsID = reader.GetInt32(reader.GetOrdinal("BudgetsID")), // Budget ID
                    BudgetName = reader.GetString(reader.GetOrdinal("BudgetName")), // Budget name
                    TotalAmount = reader.GetDouble(reader.GetOrdinal("TotalAmount")), // Total amount
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")), // User ID
                    CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")), // Category ID
                    RecurringID = reader["RecurringID"] as DateTime?, // Nullable recurring date
                    LanguageID = reader.GetInt32(reader.GetOrdinal("LanguageID")) // Language ID
                };
            }
        }

        // Populates the language dropdown list
        private void PopulateLanguageList()
        {
            using SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString());
            string query = "SELECT LanguageID, LanguageName FROM Language"; // Query to fetch languages
            SqlCommand cm = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cm.ExecuteReader();

            // Add each language to the dropdown list
            while (reader.Read())
            {
                Language.Add(new SelectListItem
                {
                    Value = reader["LanguageID"].ToString(),
                    Text = reader["LanguageName"].ToString(),
                    Selected = EditBudget.LanguageID == Convert.ToInt32(reader["LanguageID"]) // Preselect the current language
                });
            }
        }

        // Populates the category dropdown list
        private void PopulateCategoryList()
        {
            using SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString());
            string query = "SELECT CategoryID, CategoryName FROM Categories"; // Query to fetch categories
            SqlCommand cm = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cm.ExecuteReader();

            // Add each category to the dropdown list
            while (reader.Read())
            {
                Categories.Add(new SelectListItem
                {
                    Value = reader["CategoryID"].ToString(),
                    Text = reader["CategoryName"].ToString(),
                    Selected = EditBudget.CategoryID == Convert.ToInt32(reader["CategoryID"]) // Preselect the current category
                });
            }
        }
    }
}
