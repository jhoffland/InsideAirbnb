L.mapbox.accessToken = 'pk.eyJ1Ijoiam9lcGhvZmZsYW5kIiwiYSI6ImNra2Nma3Y5YTA1OTUyd3A3ZGhwaHd0djkifQ.c2nNx-yi8maJxls5Ob3fJA';
const map = L.mapbox.map('map')
    .setView([52.367912, 4.896645], 12)
    .addLayer(L.mapbox.styleLayer('mapbox://styles/mapbox/streets-v11'));

const addMarkers = respData => {
    respData.forEach(listing => {
        let popup = '';

        if (listing.hostName) {
            popup += `<div class="host">
                        <p class="host-name">${listing.hostName}</p>
                    </div>`;
        }

        popup += `<div class="main bg-light">
                    <p class="name">${listing.name}</p>`;

        if (listing.neighbourhood) {
            popup += `<p class="neighbourhood">${listing.neighbourhood}</p>`;
        }

        if (listing.roomType) {
            popup += `<p class="room-type">${listing.roomType}</p>`;
        }

        popup += '</div>';

        popup += '<div class="numbers">';

        if (listing.price) {
            popup += `<p class="price">&euro; ${listing.price}.00 /night</p>`;

            if (listing.minimumNights) {
                popup += `<p class="minimum-nights">${listing.minimumNights} night(s) minimum</p>`;
            }
        }

        if (listing.rating) {
            popup += `<p class="rating">Rating ${listing.rating / 10} / 10</p>`;

            if (listing.numberOfReviews) {
                popup += `<p class="number-of-reviews">${listing.numberOfReviews} reviews</p>`;
            }
            if (listing.lastReview) {
                const lastReview = new Date(listing.lastReview);
                popup += `<p class="last-review">Last: ${lastReview.getDate()}/${lastReview.getMonth() + 1}/${lastReview.getFullYear()}</p>`;
            }
        }

        if (listing.availability365) {
            const availabilityPerc = listing.availability365 / 365;
            popup += `<p class="high-low-availability">${highAvailability(listing.availability365) ? 'HIGH' : 'LOW'} availability</p>`;
            popup += `<p class="availability-numbers">${listing.availability365} days/year (${Math.round(availabilityPerc * 100)}%)</p>`;
        }

        popup += '</div>';

        L.marker([listing.latitude, listing.longitude], {
            icon: L.divIcon({
                className: listing.availability365 ? highAvailability(listing.availability365) ? 'marker-high' : 'marker-low' : 'marker-unknown',
                // iconSize: [60, 60]
            })
        })
            .bindPopup(popup)
            .addTo(map)
    });
}

const getFormValues = () => {
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

const updateListings = () => {
    const formValues = getFormValues();

    $('.marker-high').remove();
    $('.marker-low').remove();
    $('.marker-unknown').remove();
    $('.leaflet-popup').remove();

    $.ajax({
        url: `/Home/Listings?price-min=${formValues.priceMin}&price-max=${formValues.priceMax}&neighbourhood=${formValues.neighbourhood}&rating-min=${formValues.ratingMin}&rating-max=${formValues.ratingMax}`,
        method: "GET",
        dataType: "json"
    })
        .done(data => addMarkers(data))
        .fail(error => alert("Er is iets misgegaan met het ophalen van de locaties."));
}

document.getElementById('filter-form').addEventListener('submit', event => {
    event.preventDefault();
    updateListings();
})

updateListings();