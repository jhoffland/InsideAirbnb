// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const highAvailability = availability365 => availability365 > 70;

const getFilterFormValues = () => {
    const priceMin = document.getElementById('price-min').value;
    const priceMax = document.getElementById('price-max').value;

    const neighbourhood = document.getElementById('neighbourhood').value;

    const ratingMin = document.getElementById('rating-min').value;
    const ratingMax = document.getElementById('rating-max').value;

    return {
        priceMin,
        priceMax,
        neighbourhood,
        ratingMin,
        ratingMax
    }
}