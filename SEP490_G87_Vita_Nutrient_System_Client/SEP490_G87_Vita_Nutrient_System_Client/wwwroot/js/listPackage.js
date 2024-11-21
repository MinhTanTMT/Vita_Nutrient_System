function confirmDelete() {
    return confirm("Are you sure you want to delete this package?");
}
// Open Modal Function
function openModalAdd() {
    document.getElementById("modalOverlay").style.display = "flex";
}

function openModalUpdate(id, name, describe, price, duration) {
    var form = document.getElementById("updatePackageForm");

    form.elements['p_id'].value = id;
    form.elements['p_name'].value = name;
    form.elements['p_desc'].value = describe;
    form.elements['p_price'].value = parseInt(price);
    form.elements['p_duration'].value = duration;

    document.getElementById("modalOverlay1").style.display = "flex";
}

// Close Modal Function
function closeModal() {
    document.getElementById("modalOverlay").style.display = "none";
    document.getElementById("modalOverlay1").style.display = "none";
}

// Close modal when clicking outside
window.onclick = function (event) {
    const modalOverlay = document.getElementById("modalOverlay");
    const modalOverlay1 = document.getElementById("modalOverlay1");
    if (event.target === modalOverlay || event.target === modalOverlay1) {
        closeModal();
    }
}

function AddPackage() {
    var form = document.getElementById("addPackageForm");

    var name = form.elements['p_name'].value.trim();

    if (name === "") {
        showErrorToast("Vui lòng điền đầy đủ thông tin vào các ô.");
    } else {
        form.submit();
    }
}