@page
@model BetterSpending_Web.Pages.Budgets.AddBudgetModel
@{
    ViewData["Title"] = "Add Budget";
}

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>BetterSpendingSignin</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css">
</head>
<body class="bg-light text-center">
    <h1 class="text-success fw-bold my-4">Better Spending</h1>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-4">
                <div class="card bg-success text-white p-4 shadow">
                    <h2 class="text-warning fw-bold">Add Budget</h2>
                    <form class="mt-3" method="post">
                        <div asp-validation-summary="ModelOnly" class="text-danger mb-3"></div>

                        <div class="mb-3">
                            <label asp-for="NewBudget.BudgetName" class="form-label">Budget Name</label>
                            <input asp-for="NewBudget.BudgetName" type="text" class="form-control">
                            <span asp-validation-for="NewBudget.BudgetName" class="text-danger"></span>
                        </div>

                        <div class="mb-3">
                            <label asp-for="NewBudget.TotalAmount" class="form-label">Total Amount</label>
                            <input asp-for="NewBudget.TotalAmount" type="hidden" id="totalAmountHiddenField">
                            <input type="text" class="form-control" id="totalAmountField">
                            <span asp-validation-for="NewBudget.TotalAmount" class="text-danger"></span>
                        </div>

                        <div class="mb-3">
                            <label asp-for="NewBudget.CategoryID" class="form-label">Category</label>
                            <select asp-for="NewBudget.CategoryID" class="form-select" asp-items="Model.Categories">
                                <option value="">-- Select a Category --</option>
                            </select>
                            <span asp-validation-for="NewBudget.CategoryID" class="text-danger"></span>
                        </div>

                        <div class="mb-3">
                            <label asp-for="NewBudget.RecurringID" class="form-label">Recurring</label>
                            <input asp-for="NewBudget.RecurringID" type="date" class="form-control">
                            <span asp-validation-for="NewBudget.RecurringID" class="text-danger"></span>
                        </div>

                        <div class="mb-3">
                            <label asp-for="NewBudget.LanguageID" class="form-label">Language</label>
                            <select asp-for="NewBudget.LanguageID" class="form-select" asp-items="Model.Language">
                                <option value="">-- Select a Language --</option>
                            </select>
                            <span asp-validation-for="NewBudget.LanguageID" class="text-danger"></span>
                        </div>

                        <div class="d-grid">
                            <button type="submit" class="btn btn-primary">Add Budget</button>
                        </div>

                    </form>
                    <p class="mt-3">
                        
                    </p>
                    <a href="/" class="navigaton-link text-warning fw-bold text-decoration-none">Back to home page</a>
                </div>
            </div>
        </div>
    </div>
    <footer class="mt-4">
        <p><a href="#" class="text-success text-decoration-none">About</a></p>
        <p><a href="/Privacy" class="text-success text-decoration-none">© 2025 - BetterSpending - Privacy</a></p>
    </footer>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        const totalAmountField = document.getElementById('totalAmountField');
        const totalAmountHiddenField = document.getElementById('totalAmountHiddenField');

        // Format the value as currency
        function formatCurrency(value) {
            if (!value) return '';
            const number = parseFloat(value.replace(/[^0-9.-]+/g, ''));
            if (isNaN(number)) return '';
            return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(number);
        }

        // Remove formatting when the user focuses on the field
        totalAmountField.addEventListener('focus', (event) => {
            const value = event.target.value;
            event.target.value = value.replace(/[^0-9.-]+/g, ''); // Remove currency symbols
        });

        // Apply formatting and update the hidden field when the user exits the field
        totalAmountField.addEventListener('blur', (event) => {
            const value = event.target.value;
            const plainValue = value.replace(/[^0-9.-]+/g, ''); // Remove formatting
            totalAmountHiddenField.value = plainValue; // Update the hidden field
            event.target.value = formatCurrency(plainValue); // Display formatted value
        });

        // Format the field on page load
        document.addEventListener('DOMContentLoaded', () => {
            const value = totalAmountHiddenField.value;
            totalAmountField.value = formatCurrency(value);
        });

    </script>

</body>
</html>