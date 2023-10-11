// DELETE INCOME
var incomeIdToDelete;

document.querySelectorAll('.delete-btn').forEach(button => {
	button.addEventListener('click', function () {
		incomeIdToDelete = this.getAttribute('delete-income-id');
		document.getElementById('DeleteIncomeId').value = incomeIdToDelete;
		console.log(incomeIdToDelete);
	});
});


document.getElementById('ConfirmDeleteIncomeBtn').addEventListener('click', function () {
	if (incomeIdToDelete) {
		document.getElementById('DeleteIncomeId').value = incomeIdToDelete;
		document.getElementById('deleteIncomeForm').submit();
		incomeIdToDelete = null;
	}
	$('#deleteIncomeModal').modal('hide');
});



document.getElementById('IncomeCommand.IncomeValue').addEventListener('input', function () {
	var valueInput = this;
	var errorSpan = document.getElementById('valueError');

	var value = parseFloat(valueInput.value);
	if (isNaN(value) || value <= 0) {
		errorSpan.style.display = 'inline';
	} else {
		errorSpan.style.display = 'none';
	}
});

// ADD INCOME

var valueInput = document.getElementById('IncomeCommand.IncomeValue');
var errorSpan = document.getElementById('valueError');
var createButton = document.getElementById('createButton');

valueInput.addEventListener('input', function () {
	var value = parseFloat(valueInput.value);
	if (isNaN(value) || value <= 0) {
		errorSpan.style.display = 'inline';
		createButton.disabled = true;
	} else {
		errorSpan.style.display = 'none';
		createButton.disabled = false;
	}
});


document.getElementById('IncomeCommand.IncomeType').addEventListener('change', function () {
	var selectedValue = this.value;
	var validationMessage = document.getElementById('categoryValidationMessage');

	if (!selectedValue) {
		validationMessage.textContent = 'Please choose an income category.';
		document.getElementById('SaveChangesButton').disabled = true;
	} else {
		validationMessage.textContent = '';
		document.getElementById('SaveChangesButton').disabled = true;
	}
});

// EDIT INCOME



var incomeIdToEdit;

document.querySelectorAll('.edit-btn').forEach(button => {
	button.addEventListener('click', function () {
		incomeIdToEdit = this.getAttribute('data-income-id');
		console.log("Edycja : " + incomeIdToEdit);
	});
});


document.getElementById('IncomeType').addEventListener('change', function () {
	var editedCategory = this.value;
	var selectedCategoryId = this.options[this.selectedIndex].getAttribute('data-category-id');
	console.log("Edited Category: " + editedCategory);
	console.log("Selected Category ID: " + selectedCategoryId);
});

document.getElementById('IncomeDate').addEventListener('change', function () {
	var editedDate = this.value;
	console.log("Edited Date: " + editedDate);
});

document.getElementById('IncomeValue').addEventListener('input', function () {
	var editedValue = this.value;
	console.log("Edited Value: " + editedValue);
});

document.getElementById('EditIncomeBtn').addEventListener('click', function () {

	var incomeId = incomeIdToEdit;
	var incomeType = document.getElementById('IncomeType').value;
	var incomeCategoryId = document.getElementById('IncomeType').options[document.getElementById('IncomeType').selectedIndex].getAttribute('data-category-id');
	var incomeDate = document.getElementById('IncomeDate').value;
	var incomeValue = document.getElementById('IncomeValue').value;

	console.log("Income ID: " + incomeId);
	console.log("Income Type: " + incomeType);
	console.log("Income Category ID: " + incomeCategoryId);
	console.log("Income Date: " + incomeDate);
	console.log("Income Value: " + incomeValue);

	var incomeIdField = document.getElementById('IncomeId');
	console.log("Income ID FIELd: " + incomeIdField);

	var incomeCategoryIdField = document.getElementById('IncomeCategoryId');
	console.log("Income CATEGORYFIELD: " + incomeCategoryIdField);

	incomeIdField.value = incomeId;
	incomeCategoryIdField.value = incomeCategoryId;

	var data = {
		IncomeId: incomeId,
		IncomeType: incomeType,
		IncomeCategoryId: incomeCategoryId,
		IncomeDate: incomeDate,
		IncomeValue: incomeValue
	};

});



var categoryIdToDelete;
var categoryIdToEdit;

document.querySelectorAll('.delete-btn').forEach(button => {
	button.addEventListener('click', function () {
		categoryIdToDelete = this.getAttribute('data-category-id');
		document.getElementById('categoryIdInput').value = categoryIdToDelete;
	});
});

document.querySelectorAll('.edit-btn').forEach(button => {
	button.addEventListener('click', function () {
		var categoryId = this.getAttribute('data-category-id');
		console.log("Clicked Edit Button for Category ID: " + categoryId);
		categoryIdToEdit = categoryId;
	});
});

document.getElementById('EditBTN').addEventListener('click', function () {
	if (categoryIdToEdit) {
		var newCategoryName = document.getElementById('newCategoryName').value;
		console.log("tutaj numer: " + categoryIdToEdit);
		document.getElementById('categoryId').value = categoryIdToEdit;
		editCategory(categoryIdToEdit, newCategoryName);
		console.log("zakonczono edycje " + newCategoryName);
		console.log(categoryIdToEdit + "kuniec");
	}
	$('#editCategoryModal').modal('hide');
});



//--------- DELETE CATEGORY --------------//


document.getElementById('confirmDeleteBtn').addEventListener('click', function () {
	if (categoryIdToDelete) {
		deleteCategory(categoryIdToDelete);
		categoryIdToDelete = null;
	}
	$('#deleteCategoryModal').modal('hide');
});



//--------- EDIT CATEGORY --------------//


document.getElementById('addCategoryForm').addEventListener('submit', async function (event) {
	event.preventDefault();

	var categoryName = document.getElementById('CategoryName').value;

	var data = {
		CreateIncomeCategoryCommand: {
			CategoryName: categoryName
		}
	};

	if (response.ok) {
		var newCategoriesHtml = await response.text();
		var categoriesContainer = document.querySelector('.row');
		categoriesContainer.innerHTML = newCategoriesHtml;
		document.getElementById('CategoryName').value = '';
		document.getElementById('categoryNameError').innerText = '';
	} else {
		const errors = await response.json();
		var errorSpan = document.getElementById('categoryNameError');
		errorSpan.innerText = errors["CategoryName"][0];
	}
});

// DELETE EXPENSE
var expenseIdToDelete;

document.querySelectorAll('.delete-btn').forEach(button => {
	button.addEventListener('click', function () {
		expenseIdToDelete = this.getAttribute('delete-income-id');
		document.getElementById('DeleteExpense').value = expenseIdToDelete;
		console.log(expenseIdToDelete);
	});
});


document.getElementById('ConfirmDelete').addEventListener('click', function () {
	if (expenseIdToDelete) {
		document.getElementById('DeleteExpense').value = expenseIdToDelete;
		document.getElementById('deleteExpenseForm').submit();
		expenseIdToDelete = null;
	}
	$('#deleteExpenseModal').modal('hide');
});




// AADDD EXPENSE //

var errorSpan = document.getElementById('valueError');
var createButton = document.getElementById('createButton');
var valueInput = document.getElementById('ExpenseCommand.ExpenseValue');
var validationMessage = document.getElementById('categoryValidationMessage');

document.getElementById('ExpenseCommand.ExpenseType').addEventListener('change', function () {
	var selectedValue = this.value;


	if (!selectedValue) {
		validationMessage.textContent = 'Please choose an income category.';
		document.getElementById('SaveChangesButton').disabled = true;
	} else {
		validationMessage.textContent = '';
		document.getElementById('SaveChangesButton').disabled = true;
	}
});
document.getElementById('ExpenseCommand.ExpenseValue').addEventListener('input', function () {

	var value = parseFloat(valueInput.value);
	if (isNaN(value) || value <= 0) {
		errorSpan.style.display = 'inline';
	} else {
		errorSpan.style.display = 'none';
	}
});
var createButton = document.getElementById('createButton');

valueInput.addEventListener('input', function () {
	var value = parseFloat(valueInput.value);
	if (isNaN(value) || value <= 0) {
		errorSpan.style.display = 'inline';
		createButton.disabled = true;
	} else {
		errorSpan.style.display = 'none';
		createButton.disabled = false;
	}
});

