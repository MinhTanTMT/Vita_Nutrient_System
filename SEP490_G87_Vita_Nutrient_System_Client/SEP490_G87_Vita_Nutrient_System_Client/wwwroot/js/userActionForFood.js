async function toggleLike(userId, foodId) {
    try {
        const response = await fetch("https://localhost:7045/api/UserFoodAction/UserLikeOrUnlikeFood", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ userId: userId, foodId: foodId })
        });

        if (response.ok) {
            var likeIcon = document.getElementById("like-icon");
            if (likeIcon.classList.contains("mdi-thumb-up-outline")) {
                likeIcon.classList.remove("mdi-thumb-up-outline");
                likeIcon.classList.add("mdi-thumb-up");
            } else {
                likeIcon.classList.remove("mdi-thumb-up");
                likeIcon.classList.add("mdi-thumb-up-outline");
            }
        } else {
            showToast("Unexpected error occurred!");
        }
    } catch (error) {
        console.error("Error:", error);
        showToast("Unexpected error occurred!");
    }
}

async function toggleSave(userId, foodId) {
    try {
        const response = await fetch("https://localhost:7045/api/UserFoodAction/UserSaveOrUnsaveFood", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ userId: userId, foodId: foodId })
        });

        if (response.ok) {
            var saveIcon = document.getElementById("save-icon");
            if (saveIcon.classList.contains("mdi-bookmark-outline")) {
                saveIcon.classList.remove("mdi-bookmark-outline");
                saveIcon.classList.add("mdi-bookmark");
            } else {
                saveIcon.classList.remove("mdi-bookmark");
                saveIcon.classList.add("mdi-bookmark-outline");
            }
        } else {
            showToast("Unexpected error occurred!");
        }
    } catch (error) {
        console.error("Error:", error);
        showToast("Unexpected error occurred!");
    }
}

async function blockFood(userId, foodId) {
    try {
        const response = await fetch("https://localhost:7045/api/UserFoodAction/UserBlockFood", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ userId: userId, foodId: foodId })
        });

        if (response.ok) {
            window.location.href = "https://localhost:7069/foodsList";
        } else {
            showToast("Unexpected error occurred!");
        }
    } catch (error) {
        console.error("Error:", error);
        showToast("Unexpected error occurred!");
    }
}

function showToast(message) {
    const toast = document.createElement("div");
    toast.className = "toast-message";
    toast.textContent = message;

    toast.style.position = "fixed";
    toast.style.top = "50px";
    toast.style.right = "20px";
    toast.style.padding = "10px 20px";
    toast.style.backgroundColor = "#333";
    toast.style.color = "#fff";
    toast.style.borderRadius = "5px";
    toast.style.zIndex = "1000";
    toast.style.opacity = "0";
    toast.style.transition = "opacity 0.5s ease";

    document.body.appendChild(toast);

    setTimeout(() => (toast.style.opacity = "1"), 100);

    setTimeout(() => {
        toast.style.opacity = "0";
        setTimeout(() => document.body.removeChild(toast), 500);
    }, 3000);
}