const errMsgInput = document.querySelector('input[name="errMsg"]').value;
const sucMsgInput = document.querySelector('input[name="successMsg"]').value;

if (errMsgInput) {
    showErrorToast(errMsgInput);
}

if (sucMsgInput) {
    showSuccessToast(sucMsgInput);
}
function showSuccessToast(msg) {
    toast({
        title: "Successful!",
        message: msg,
        type: "success",
        duration: 3000
    });
}

function showErrorToast(msg) {
    toast({
        title: "Failed!",
        message: msg,
        type: "error",
        duration: 3000
    });
}

// Toast function
function toast({ title = "", message = "", type = "info", duration = 3000 }) {
    const main = document.getElementById("toast");
    if (main) {
        const toast = document.createElement("div");

        // Auto remove toast
        const autoRemoveId = setTimeout(function () {
            main.removeChild(toast);
        }, duration + 1000);

        // Remove toast when clicked
        toast.onclick = function (e) {
            if (e.target.closest(".toast__close")) {
                main.removeChild(toast);
                clearTimeout(autoRemoveId);
            }
        };

        const icons = {
            success: "mdi mdi-information",
            error: "mdi mdi-alert"
        };
        const icon = icons[type];
        const delay = (duration / 1000).toFixed(2);

        toast.classList.add("toast", "show", `toast--${type}`);
        toast.style.animation = `slideInLeft ease .3s, fadeOut linear 1s ${delay}s forwards`;

        toast.innerHTML = `
                    <div class="toast__icon">
                        <i class="${icon}"></i>
                    </div>
                    <div class="toast__body">
                        <h3 class="toast__title">${title}</h3>
                        <p class="toast__msg">${message}</p>
                    </div>
                `;
        main.appendChild(toast);
    }
}

