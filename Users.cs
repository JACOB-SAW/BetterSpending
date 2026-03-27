using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BetterSpending_Web.Model
{
    public class Registration
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [PasswordValidation(ErrorMessage ="Password must be at least 10 characters long, contain at least one number, and have a Upper Case letter")]
        public string Password { get; set; }

        //fixed page
    }
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string password)
            {
                // Check if the password meets the requirements
                if (password.Length >= 10 &&
                    Regex.IsMatch(password, @"[A-Z]") && // At least one uppercase letter
                    Regex.IsMatch(password, @"\d"))     // At least one number
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage ?? "Invalid password.");
        }
    }
}