google.charts.load('current', { 'packages': ['corechart'] });

let roomTypeChart;
let availabilityChart;

const drawRoomTypeChart = respData => {
    const chartData = new google.visualization.DataTable();
    chartData.addColumn('string', 'Room type');
    chartData.addColumn('number', 'Listings');

    chartData.addRows(respData.reduce((prev, curr) => {
        prev.push([curr.roomType, curr.amount])
        return prev;
    }, []));

    roomTypeChart.draw(chartData, {
        'title': 'Room types',
        // 'width': 400,
        'height': 300,
        'legend': { 'position': 'bottom', 'alignment': 'center' }
    });
}

const updateRoomTypeChart = formValues => {
    $.ajax({
        url: `/Home/RoomTypeStats?price-min=${formValues.priceMin}&price-max=${formValues.priceMax}&neighbourhood=${formValues.neighbourhood}&rating-min=${formValues.ratingMin}&rating-max=${formValues.ratingMax}`,
        method: "GET",
        dataType: "json"
    })
        .done(data => drawRoomTypeChart(data))
        .fail(error => alert("Something went wrong with retreiving the room type stats."));
}

const drawAvailabilityChart = respData => {
    const chartData = new google.visualization.DataTable();
    chartData.addColumn('string', 'Availability');
    chartData.addColumn('number', 'Listings');

    chartData.addRows(respData.reduce((prev, curr) => {
        if (highAvailability(curr.availability365)) {
            prev[0][1] += curr.amount;
        } else {
            prev[1][1] += curr.amount;
        }
        
        return prev;
    }, [
        ["High", 0],
        ["Low", 0]
    ]));

    availabilityChart.draw(chartData, {
        'title': 'Availability',
        // 'width': 400,
        'height': 300,
        'legend': { 'position': 'bottom', 'alignment': 'center' }
    });
}

const updateAvailabilityChart = formValues => {
    $.ajax({
        url: `/Home/AvailabilityStats?price-min=${formValues.priceMin}&price-max=${formValues.priceMax}&neighbourhood=${formValues.neighbourhood}&rating-min=${formValues.ratingMin}&rating-max=${formValues.ratingMax}`,
        method: "GET",
        dataType: "json"
    })
        .done(data => drawAvailabilityChart(data))
        .fail(error => alert("Something went wrong with retreiving the availability stats."));
}

const updateCharts = () => {
    const formValues = getFilterFormValues();

    updateRoomTypeChart(formValues);
    updateAvailabilityChart(formValues);
}

google.charts.setOnLoadCallback(() => {
    roomTypeChart = new google.visualization.BarChart(document.getElementById('room-type-chart'));
    availabilityChart = new google.visualization.PieChart(document.getElementById('availability-chart'));

    document.getElementById('filter-form').addEventListener('submit', event => {
        event.preventDefault();
        updateCharts();
    })

    updateCharts();
});