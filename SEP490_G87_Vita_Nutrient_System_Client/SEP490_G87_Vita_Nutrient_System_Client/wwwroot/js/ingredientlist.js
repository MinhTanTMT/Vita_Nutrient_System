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