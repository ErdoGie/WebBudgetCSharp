flatpickr('#IncomeDate', {
    enableTime: false,
    dateFormat: 'Y-m-d',
});

document.getElementById("IncomeDate").addEventListener("change", function () {
    var selectedDate = new Date(this.value);
    var currentDate = new Date();
    var minDate = new Date(2000, 0, 1);
    var maxDate = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0);
    var validationMessage = document.getElementById("validationDateMessage");

    if (selectedDate >= minDate && selectedDate <= maxDate) {
        validationMessage.textContent = "Date validation correct.";
        validationMessage.style.color = "#04b524";
    } else {
        validationMessage.textContent = "Date must be between 2000-01-01 and the end of the current month.";
        validationMessage.style.color = "#fc002e";
    }
});

var incomeTypeInput = document.getElementById("IncomeType");
var toggleButton = document.getElementById("toggleIncomeOptions");
var incomeTypeOptions = document.getElementById("IncomeTypeOptions");

toggleButton.addEventListener("click", function () {
    incomeTypeOptions.style.display = incomeTypeOptions.style.display === "none" ? "block" : "none";
    if (incomeTypeOptions.style.display === "block") {
        document.querySelectorAll("#IncomeTypeOptions input[type='radio']").forEach(function (radio) {
            radio.addEventListener("change", function () {
                if (this.value === "Another") {
                    incomeTypeInput.removeAttribute("readonly");
                    incomeTypeInput.value = "";
                } else {
                    incomeTypeInput.setAttribute("readonly", true);
                    incomeTypeInput.value = this.value;
                }
                incomeTypeOptions.style.display = "none";
            });
        });
    }
});

var expenseValueInput = document.getElementById("IncomeValue");
var expenseValueValidationMessage = document.getElementById("incomeValueValidationMessage");

expenseValueInput.addEventListener("input", function () {
    var value = parseFloat(expenseValueInput.value);
    if (isNaN(value) || value < 0) {
        expenseValueValidationMessage.textContent = "Value must be a non-negative number.";
        expenseValueValidationMessage.style.color = "#fc002e";
    } else {
        expenseValueValidationMessage.textContent = "";
    }
});

//expense






// main 
if (window.location.pathname === '/') {
   
    var elementToAnimate = document.getElementById('Index.cshtml'); 

    elementToAnimate.classList.add('slideDownAnimation');
}