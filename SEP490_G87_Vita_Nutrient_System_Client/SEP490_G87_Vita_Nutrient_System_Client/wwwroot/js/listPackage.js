function confirmDelete() {
    return confirm("Are you sure you want to delete this package?");
}
// Open Modal Function
function openModalAdd() {
    document.getElementById("modalOverlay").style.display = "flex";
}

function openModalUpdate(id, name, describe, price, duration, nutritionists, baseAPIAddress) {
    var form = document.getElementById("updatePackageForm");
    var form1 = document.getElementById("updatePackageNutriForm");

    form1.elements['p_id'].value = id;
    form.elements['p_id'].value = id;
    form.elements['p_name'].value = name;
    form.elements['p_desc'].value = describe;
    form.elements['p_price'].value = parseInt(price);
    form.elements['p_duration'].value = parseInt(duration);

    updateNutriTable(nutritionists);

    document.getElementById("modalOverlay1").style.display = "flex";
}

function updateNutriTable(input, baseAPIAddress) {
    const nutriTable = document.getElementById("nutriTable");
    const selectElement = document.getElementById("nutritionistsSelect");

    nutriTable.innerHTML = '';

    if (input.length === 0) {
        const emptyMessage = document.createElement("span");
        emptyMessage.setAttribute("id", "no-nutri-msg");
        emptyMessage.textContent = "There is no nutritionist!";

        nutriTable.appendChild(emptyMessage);
        return;
    }

    // Duyệt qua từng phần tử trong mảng input
    input.forEach(item => {
        const optionId = `opt-${item.Id}`;

        const optionElement = document.getElementById(optionId);

        if (optionElement) {
            optionElement.style.display = "none";
            if (selectElement.value === optionElement.value) {
                const visibleOption = Array.from(selectElement.options).find(
                    option => option.style.display !== "none"
                );

                if (visibleOption) {
                    selectElement.value = visibleOption.value;
                }
            }
        }
        //////////////
        const nutritionistDiv = document.createElement("div");
        nutritionistDiv.className = "nutritionist";
        nutritionistDiv.setAttribute("id",`rec_${item.Id}`);
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
        n2Div.setAttribute("onclick", `deleteNutritionist(${item.Id}, '${baseAPIAddress}');`);

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
    ///////////
    const selectElement = document.getElementById("nutritionistsSelect");

    const options = selectElement.getElementsByTagName("option");

    Array.from(options).forEach(option => {
        option.style.display = "block";
    });
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
    var desc = form.elements['p_desc'].value.trim();
    var price = form.elements['p_price'].value;
    var duration = form.elements['p_duration'].value;

    if (name === "" || desc === "") {
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

function AddNutri(baseAPIAddress) {
    const selectElement = document.getElementById("nutritionistsSelect");
    const selectedOption = selectElement.options[selectElement.selectedIndex];
    const pid = document.getElementById("updatePackageForm").elements['p_id'].value;
    const nid = selectedOption.value;
    const parts = selectedOption.text.split(" - ");

    fetch(baseAPIAddress + `/ExpertPackage/AddNutritionistToPackage/${nid}/${pid}`, {
        method: 'GET'
    })
        .then(response => {
            if (response.ok) {
                closeModal();
                showSuccessToast("Add nutritionist to package successful!");
                setTimeout(() => {
                    location.reload()
                }, 1000);
            } else {
                showErrorToast("Add nutritionist to package failed!");
            }
        })
        .catch(error => {
            console.error("Error:", error);
            showErrorToast("Add nutritionist to package failed!");
        });
}

function deleteNutritionist(Id, baseAPIAddress) {
    fetch(baseAPIAddress + `/ExpertPackage/RemoveNutritionistFromPackage/${Id}`, {
        method: 'GET'
    })
        .then(response => {
            if (response.ok) {
                const elementToRemove = document.getElementById(`rec_${Id}`);
                if (elementToRemove) {
                    elementToRemove.remove();
                    showOption(Id);
                }
                closeModal();
                showSuccessToast("Remove successful!");
                setTimeout(() => {
                    location.reload()
                }, 1000);
            } else {
                showErrorToast("Remove failed!");
            }
        })
        .catch(error => {
            console.error("Error:", error);
            showErrorToast(error);
        });
}

function showOption(id) {
    const selectElement = document.getElementById("nutritionistsSelect");

    const options = selectElement.getElementsByTagName("option");

    Array.from(options).forEach(option => {

        if (option.attributes.id.value === "opt-" + id) {
            option.style.display = "block";
        }
    });
}

function hideOption(id) {
    const selectElement = document.getElementById("nutritionistsSelect");

    const options = selectElement.getElementsByTagName("option");

    Array.from(options).forEach(option => {

        if (option.attributes.id.value === "opt-" + id) {
            option.style.display = "none";
        }
    });
}

function selectVisibleOption() {
    const selectElement = document.getElementById("nutritionistsSelect");
    const options = selectElement.options;

    for (let i = 0; i < options.length; i++) {
        const option = options[i];

        if (option.style.display !== "none") {
            selectElement.selectedIndex = i;
            break;
        }
    }
}

