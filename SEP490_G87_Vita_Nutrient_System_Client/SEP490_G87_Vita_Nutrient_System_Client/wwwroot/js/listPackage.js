function confirmDelete() {
    return confirm("Are you sure you want to delete this package?");
}
// Open Modal Function
function openModalAdd() {
    document.getElementById("modalOverlay").style.display = "flex";
}

function openModalUpdate(id, name, describe, price, duration, nutritionists) {
    var form = document.getElementById("updatePackageForm");

    form.elements['p_id'].value = id;
    form.elements['p_name'].value = name;
    form.elements['p_desc'].value = describe;
    form.elements['p_price'].value = parseInt(price);
    form.elements['p_duration'].value = parseInt(duration);

    updateNutriTable(nutritionists);

    document.getElementById("modalOverlay1").style.display = "flex";
}

function updateNutriTable(input) {
    const nutriTable = document.getElementById("nutriTable");

    nutriTable.innerHTML = '';

    if (input.length === 0) {
        const emptyMessage = document.createElement("span");
        emptyMessage.textContent = "There is no nutritionist!";

        nutriTable.appendChild(emptyMessage);
        return;
    }

    // Duyệt qua từng phần tử trong mảng input
    input.forEach(item => {
        const nutritionistDiv = document.createElement("div");
        nutritionistDiv.className = "nutritionist";

        const n1Div = document.createElement("div");
        n1Div.className = "n1";

        const nameSpan = document.createElement("span");
        nameSpan.className = "nname";
        nameSpan.textContent = item.Name;

        const accountSpan = document.createElement("span");
        accountSpan.className = "nacc";
        accountSpan.textContent = item.Account;

        n1Div.appendChild(nameSpan);
        n1Div.appendChild(accountSpan);

        const n2Div = document.createElement("div");
        n2Div.className = "n2";
        n2Div.setAttribute("onclick", `deleteNutritionist(${item.Id});`);

        const deleteIcon = document.createElement("i");
        deleteIcon.className = "mdi mdi-delete";

        n2Div.appendChild(deleteIcon);

        nutritionistDiv.appendChild(n1Div);
        nutritionistDiv.appendChild(n2Div);

        nutriTable.appendChild(nutritionistDiv);
    });
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
    var price = form.elements['p_price'].value;
    var duration = form.elements['p_duration'].value;

    if (name === "") {
        showErrorToast("Vui lòng điền đầy đủ thông tin vào các ô.");
    } else if (price < 0) {
        showErrorToast("Price phải lớn hơn hoặc bằng 0.");
    } else if (duration < 0) {
        showErrorToast("Duration phải lớn hơn hoặc bằng 0.");
    } else {
        form.submit();
    }
}

function EditPackage() {
    var form = document.getElementById("updatePackageForm");

    var name = form.elements['p_name'].value.trim();
    var price = form.elements['p_price'].value;
    var duration = form.elements['p_duration'].value;

    if (name === "") {
        showErrorToast("Vui lòng điền đầy đủ thông tin vào các ô.");
    } else if (price < 0) {
        showErrorToast("Price phải lớn hơn hoặc bằng 0.");
    } else if (duration < 0) {
        showErrorToast("Duration phải lớn hơn hoặc bằng 0.");
    } else {
        form.submit();
    }
}

function openTab(event, tabId) {
    var tabButtons = document.querySelectorAll(".tab-button");
    var tabContents = document.querySelectorAll(".tab-content");

    tabButtons.forEach(button => button.classList.remove("active"));
    tabContents.forEach(content => content.classList.remove("active"));

    event.currentTarget.classList.add("active");
    document.getElementById(tabId).classList.add("active");
}

function deleteNutritionist(Id) {
    console.log("delete " + id +"~~")
}