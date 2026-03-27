namespace BetterSpending_Web.Model
{
    public class BudgetView
    {
        public int BudgetsID { get; set; }

        public string BudgetName { get; set; }

        public decimal TotalAmount { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public DateTime? RecurringID { get; set; } // Nullable for optional recurring dates

        public string LanguageName { get; set; }

        public string CategoryName { get; set; }

        public string CategoryType { get; set; }

    }
}
