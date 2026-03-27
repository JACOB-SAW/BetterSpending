using System.ComponentModel.DataAnnotations;

namespace BetterSpending_Web.Model
{
    public class BudgetModel
    {
        [Required(ErrorMessage = "Budget Name is Required")]
        public string BudgetName { get; set; }

        [Required(ErrorMessage = "Budget Amount is Required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total Amount must be a valid number.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Budget Start Date is Required")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Category Required")]
        public int CategoryID { get; set; }

        public DateTime? RecurringID { get; set; } // Nullable for optional recurring dates

        [Required(ErrorMessage = "Language Required")]
        public int LanguageID { get; set; }

        public char CategoryName { get; set; }

        public char CategoryType { get; set; }
    }
}
