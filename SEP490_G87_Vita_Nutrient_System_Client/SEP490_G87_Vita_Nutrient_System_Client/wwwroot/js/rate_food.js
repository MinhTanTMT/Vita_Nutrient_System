const stars = document.querySelectorAll('.star1');

stars.forEach(star => {
    star.addEventListener('mouseover', () => {
        const index = parseInt(star.getAttribute('data-star'));

        stars.forEach((s, i) => {
            s.classList.toggle('active', i < index);
            s.classList.remove('inactive', i < index);
        });

        stars.forEach((s, i) => {
            s.classList.toggle('inactive', i >= index);
        });
    });

    star.addEventListener('mouseout', () => {
        const index = parseInt(star.getAttribute('data-star'));
        stars.forEach(s => s.classList.remove('active'));
        stars.forEach(s => s.classList.remove('inactive'));
        stars.forEach((s, i) => {
            if (!s.classList.contains('active2')) {
                s.classList.toggle('inactive', i < index);
            }
        });
    });
});

async function handleRating(userId, foodId, rating) {
    try {
        const response = await fetch("https://localhost:7045/api/UserFoodAction/UserRateFood", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ userId: userId, foodId: foodId, rate: rating })
        });

        if (!response.ok) {
            showToast("Unexpected error occurred!");
        } else {
            window.location.href = window.location.href;
            history.go(0);
            location.reload();
        }
    } catch (error) {
        console.error("Error:", error);
        showToast("Unexpected error occurred!");
    }
}
