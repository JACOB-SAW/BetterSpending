using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BetterSpending_Web.Model;
using Microsoft.Data.SqlClient;
using BetterSpending_Business;

namespace BetterSpending_Web.Pages
{
    public class IndexModel : PageModel
    {
        public List<IndexBudget> Budgets { get; set; } = new List<IndexBudget>();

        public void OnGet()
        {
            PopulateBudget();
        }

        public void PopulateBudget()
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetConnectionString()))
            {
                string cmdText = "SELECT b.BudgetsID, b.BudgetName, b.TotalAmount, b.CategoryID, b.RecurringID, " +
                    "c.CategoryName, c.CategoryType " +
                    "FROM Budgets b " +
                    "JOIN Categories c ON b.CategoryID = c.CategoryID " +
                    "ORDER BY NEWID()";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    IndexBudget budget = new IndexBudget
                    {
                        BudgetID = Convert.ToInt32(reader["BudgetsID"]),
                        BudgetName = reader["BudgetName"].ToString(),
                        BudgetAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        CategoryID = reader["CategoryID"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        RecurringID = reader["RecurringID"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["RecurringID"])

                    };
                    Budgets.Add(budget);
                }
            }
        }
    }
}

