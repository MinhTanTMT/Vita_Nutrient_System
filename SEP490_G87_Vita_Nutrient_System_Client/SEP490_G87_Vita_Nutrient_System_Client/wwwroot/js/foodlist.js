// dropdown menus

const dropdowns = document.querySelectorAll('.dropdown')

function closeDropdowns() {
    dropdowns.forEach(item => {
        item.classList.remove('dropdown-menu-visible')
    })
}

dropdowns.forEach(item => {
    item.addEventListener('click', () => {
        if (!item.classList.contains('dropdown-menu-visible')) {
            closeDropdowns();
        };
        item.classList.toggle('dropdown-menu-visible')
    })
})

window.addEventListener('click', (e) => {
    if (!e.target.classList.contains('dropdown') && !e.target.closest('.dropdown')) {
        closeDropdowns()
    }
})

    function updateFoodType(foodTypeId, foodTypeName) {
        document.querySelector('input[name="foodTypeId"]').value = foodTypeId;
        document.getElementById("typeFilterButton").innerText = foodTypeName;
    }
