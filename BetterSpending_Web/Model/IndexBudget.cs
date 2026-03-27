namespace BetterSpending_Web.Model
{
    public class IndexBudget
    {
        public int BudgetID { get; set; }

        public string BudgetName { get; set; }

        public decimal BudgetAmount { get; set; }

        public string CategoryID { get; set; }
        public string CategoryName { get; set; }

        public DateTime? RecurringID { get; set; }
    }
}
