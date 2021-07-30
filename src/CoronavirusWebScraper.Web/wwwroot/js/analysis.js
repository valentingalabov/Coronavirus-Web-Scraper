import ApiGetFunction from "./getFunction.js";
google.charts.load('current', { 'packages': ['corechart'] });


document.getElementById("chart-select").addEventListener("change", draw, { passive: true });

let statistics = await ApiGetFunction("https://localhost:44305/api/Data/analysis");

var divToDraw = document.getElementById('charts');

function draw() {
    let selectElement = document.getElementById("chart-select");
    let title = selectElement.options[selectElement.value].textContent;

    if (selectElement.value == 1) {
        google.charts.setOnLoadCallback(drawPieChart(title, statistics.active, statistics.hospitalized));
    } else if (selectElement.value == 2) {
        google.charts.setOnLoadCallback(drawPieChart(title, statistics.hospitalized, statistics.icu));
    } else if (selectElement.value == 3) {
        google.charts.setOnLoadCallback(drawPieChart(title, statistics.infected, statistics.totalTests));
    }
    else {
        document.getElementById("charts").innerHTML = "";
    }
}

function drawPieChart(title, el1, el2) {
    let chartData = title.split("/");
    var data = google.visualization.arrayToDataTable([
        [title, 'брой'],
        [chartData[0], el1],
        [chartData[1], el2]
    ]);

    var options = {
        pieSliceText: 'label',
        title: title,
        pieStartAngle: 100,
        is3D: true
    };

    var chart = new google.visualization.PieChart(divToDraw);

    chart.draw(data, options);
}

