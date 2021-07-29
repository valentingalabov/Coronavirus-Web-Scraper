import ApiGetFunction from "./getFunction.js";

google.charts.load('current', { 'packages': ['corechart'] });


document.getElementById("chart-select").addEventListener("change", draw);

var divToDraw = document.getElementById('charts')

let stats = await ApiGetFunction("https://localhost:44305/api/Data/analysis");
console.log(stats);
async function draw() {
    let val = document.getElementById("chart-select").value;


    if (val == 1) {
        google.charts.setOnLoadCallback(await drawActiveHospitalized(stats.active, stats.hospitalized));
    } else if (val == 2) {
        google.charts.setOnLoadCallback(await drawHospitalizedIcu(stats.hospitalized, stats.icu));
    } else if (val == 3) {
        google.charts.setOnLoadCallback(await drawTotalConfMedical);

    } else if (val == 4) {
        google.charts.setOnLoadCallback(await drawDiseasedTested(stats.infected, stats.tested));
    }
    else {
        document.getElementById("charts").innerHTML = "";
    }
}

function drawActiveHospitalized(active, hospitalized) {

    var data = google.visualization.arrayToDataTable([
        ['Активни/Хоспитализирани', 'брой'],
        ['Активни', active],
        ['Хоспитализирани', hospitalized]
    ]);

    var options = {
        pieSliceText: 'label',
        title: 'Активни случаи към хоспитализирани',
        pieStartAngle: 100,
        is3D: true
    };

    var chart = new google.visualization.PieChart(divToDraw);

    chart.draw(data, options);
}

function drawHospitalizedIcu(hospitalized, icu) {

    var data = google.visualization.arrayToDataTable([
        ['Хоспитализирани/Интензивно отделение', 'брой'],
        ['Хоспитализирани', hospitalized],
        ['В интензивно отделение', icu]
    ]);

    var options = {
        pieSliceText: 'label',
        title: 'Хоспитализирани/В интензивно отделение',
        pieStartAngle: 100,
        is3D: true
    };
  
    var chart = new google.visualization.PieChart(divToDraw);

    chart.draw(data, options);
}

//function drawTotalConfMedical() {

//    var data = google.visualization.arrayToDataTable([
//        ['Тип', 'брой'],
//        ['Лекари', 3864],
//        ['Медицински сестри', 4559],
//        ['Санитари', 2255],
//        ['Фелдшери', 290],
//        ['Други', 2501],
//    ]);

//    var options = {
//        pieSliceText: 'label',
//        title: 'Потвърдени случаи за медицински персонал',
//        pieStartAngle: 100,
//        is3D: true
//    };

//    var chart = new google.visualization.PieChart(document.getElementById('charts'));

//    chart.draw(data, options);
//}

function drawDiseasedTested(infected, tested) {

    var data = google.visualization.arrayToDataTable([
        ['Заразени/Тествани', 'брой'],
        ['Заразени', 424295],
        ['Тествани', 3574097],
    ]);

    var options = {
        pieSliceText: 'label',
        title: 'Заразени / Тестванил',
        pieStartAngle: 100,
        is3D: true
    };

    var chart = new google.visualization.PieChart(divToDraw);

    chart.draw(data, options);
}


