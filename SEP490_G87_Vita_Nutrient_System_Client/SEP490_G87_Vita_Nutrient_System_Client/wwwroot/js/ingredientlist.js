function confirmDelete()
{
    return confirm("Are you sure you want to delete this ingredient?");
}
// Open Modal Function
function openModal() {
    document.getElementById("modalOverlay").style.display = "flex";
}

// Close Modal Function
function closeModal() {
    document.getElementById("modalOverlay").style.display = "none";
}

// Close modal when clicking outside
window.onclick = function (event) {
    const modalOverlay = document.getElementById("modalOverlay");
    if (event.target === modalOverlay) {
        closeModal();
    }
}

function AddIngredient() {
    var form = document.getElementById("ingredientForm");

    var name = form.elements['in_name'].value.trim();
    var desc = form.elements['in_desc'].value.trim();

    if (name === "" || desc === "") {
        showErrorToast("Vui lòng điền đầy đủ thông tin vào các ô.");
    } else {
        form.submit();
    }
}