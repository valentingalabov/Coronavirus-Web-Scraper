import ApiGetFunction from "./getFunction.js";
google.charts.load('current', { 'packages': ['corechart'] });


document.getElementById("chart-select").addEventListener("change", draw, { passive: true });

let statistics = await ApiGetFunction("/api/analysis");

let divToDraw = document.getElementById('charts');

let dateDiv = document.getElementById('date');
let h1 = document.createElement('h1');
h1.textContent = statistics.date;
dateDiv.appendChild(h1);

/*Draw selected chart with needed data.*/
function draw() {
    let selectElement = document.getElementById("chart-select");
    let title = selectElement.options[selectElement.value].textContent;

    if (selectElement.value == 1) {
        divToDraw.innerHTML = "";
        google.charts.setOnLoadCallback(drawColumnChart(title, statistics.active, statistics.hospitalized));
    } else if (selectElement.value == 2) {
        google.charts.setOnLoadCallback(drawColumnChart(title, statistics.hospitalized, statistics.icu));
    } else if (selectElement.value == 3) {
        google.charts.setOnLoadCallback(drawColumnChart(title, statistics.confirmed, statistics.totalTests));
    } else if (selectElement.value == 4) {
        google.charts.setOnLoadCallback(drawMedicalPieChart);
    } else if (selectElement.value == 5) {
        google.charts.setOnLoadCallback(drawColumnChart(title,statistics.confirmed, statistics.totalRecovered));
    } else {
        divToDraw.innerHTML = "";
    }
}

/*Draw pie chart with given title and two elements.*/
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
        is3D: true
    };

    var chart = new google.visualization.PieChart(divToDraw);
    chart.draw(data, options);
}

/*Draw pie chart with statistical information about medical staff.*/
function drawMedicalPieChart() {
    var data = google.visualization.arrayToDataTable([
        ['Тип', 'Брой'],
        ['Доктори', statistics.totalMedicalAnalisys.doctors],
        ['Медицински сестри', statistics.totalMedicalAnalisys.nurces],
        ['Санитари', statistics.totalMedicalAnalisys.paramedics_1],
        ['Фелдшери', statistics.totalMedicalAnalisys.paramedics_2],
        ['Друг мед. персонал', statistics.totalMedicalAnalisys.other]
    ]);

    var options = {
        pieSliceText: 'label',
        title: "Потвърдени случаи за медицински персонал по тип",
        pieStartAngle: 100,
        is3D: true
    };

    var chart = new google.visualization.PieChart(divToDraw);
    chart.draw(data, options);
}

/*Draw column chart with given title and two elements.*/
function drawColumnChart(title, el1, el2) {
    let chartData = title.split("/");

    var data = google.visualization.arrayToDataTable([
        ['Тип', 'Брой', { role: 'style' }],
        [chartData[0], el1, 'red'],
        [chartData[1], el2, 'green'],
    ]);

    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
        {
            calc: "stringify",
            sourceColumn: 1,
            type: "string",
            role: "annotation"
        },
        2]);


    var options = {
        title: title,
        bar: { groupWidth: "95%" },
        legend: { position: "none" },
        vAxis: {
            viewWindow: {
                min: 0,
            }
        }
    };

    var chart = new google.visualization.ColumnChart(divToDraw);
    chart.draw(view, options);
}

