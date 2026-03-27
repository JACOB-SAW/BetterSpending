namespace BetterSpending_Web.Model
{
    public class EditBudget
    {

        public int BudgetsID { get; set; } // Unique identifier for the budget
        public string BudgetName { get; set; } // Name of the budget

        public double TotalAmount { get; set; } // Total amount allocated for the budget

        public int UserID { get; set; } // ID of the user who owns the budget

        public int CategoryID { get; set; } // ID of the category associated with the budget

        public DateTime? RecurringID { get; set; } // ID of the recurring budget type

        public int LanguageID { get; set; } // ID of the language associated with the budget
    }
}
