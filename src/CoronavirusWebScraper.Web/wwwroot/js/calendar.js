import ApiGetFunction from './getFunction.js';

let today = new Date();
let currentMonth = today.getMonth();
let currentYear = today.getFullYear();
let selectYear = document.getElementById("year");
let selectMonth = document.getElementById("month");

let months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

let monthAndYear = document.getElementById("monthAndYear");
showCalendar(currentMonth, currentYear);

/*Visulize calendar for next month.*/
async function next() {
    currentYear = (currentMonth === 11) ? currentYear + 1 : currentYear;
    currentMonth = (currentMonth + 1) % 12;
    await showCalendar(currentMonth, currentYear);
}

/*Visulize calendar for previous month.*/
async function previous() {
    currentYear = (currentMonth === 0) ? currentYear - 1 : currentYear;
    currentMonth = (currentMonth === 0) ? 11 : currentMonth - 1;
    await showCalendar(currentMonth, currentYear);
}

/*Visualize calendar for selected month and year from dropdown.*/
async function jump() {
    currentYear = parseInt(selectYear.value);
    currentMonth = parseInt(selectMonth.value);
    await showCalendar(currentMonth, currentYear);
}

/*Visualize calendar for current month and year.*/
async function currentDate() {
    await showCalendar(today.getMonth(), today.getFullYear());
}

/*Visualize calendar for given month and year.*/
async function showCalendar(month, year) {
    let path = `/api/dates?year=${year}&month=${month+1}`;
    let statisticsDates = await ApiGetFunction(path);
    let tbl = document.getElementById("calendar-body");

    tbl.innerHTML = "";

    monthAndYear.innerHTML = months[month] + " " + year;
    selectYear.value = year;
    selectMonth.value = month;

    let date = 1;
    for (let i = 0; i < 6; i++) {
        let row = document.createElement("tr");


        for (let j = 0; j < 7; j++) {
            if (i === 0 && j < getDay(new Date(year, month))) {
                let cell = document.createElement("td");
                let cellText = document.createTextNode("");
                cell.appendChild(cellText);
                row.appendChild(cell);
            }
            else if (date > daysInMonth(month, year)) {
                break;
            }
            else {
                let cell = document.createElement("td");
                cell.classList.add("calendar-numbers");

                let cellText = document.createTextNode(date);
                let currDateAtMoment = new Date(year, month, date);

                if (date === today.getDate()
                    && year === today.getFullYear()
                    && month === today.getMonth()
                    && isIncluded(statisticsDates, currDateAtMoment)) {
                    cell.classList.add("circle");
                    cell.classList.add("text-success");

                    let a = document.createElement("a");
                    a.href = `DateData?date=${year}-${month + 1}-${date}`;
                    a.appendChild(cellText);
                    a.classList.add("statistic-day");
                    cell.appendChild(a);

                } else if (date === today.getDate()
                    && year === today.getFullYear()
                    && month === today.getMonth()
                    && !isIncluded(statisticsDates, currDateAtMoment)) {
                    cell.appendChild(cellText);
                } else if (isIncluded(statisticsDates, new Date(year, month, date))) {
                    cell.classList.add("text-success");
                    let a = document.createElement("a");
                    a.href = `DateData?date=${year}-${month + 1}-${date}`;
                    a.classList.add("statistic-day");
                    a.appendChild(cellText);
                    cell.appendChild(a);
                } else {
                    cell.appendChild(cellText);
                }

                row.appendChild(cell);
                date++;
            }
        }

        tbl.appendChild(row);
    }

    function getDay(date) {
        let day = date.getDay();
        if (day == 0) day = 7;
        return day - 1;
    }
}

/*Check if returned form api list of dates contains current date.*/
function isIncluded(listOfDates, currDate) {
    try {
        var contains = listOfDates.some(elem => {
            return JSON.stringify(currDate.getTime()) === JSON.stringify(new Date(elem).getTime());
        });
        if (contains) {
            return true;
        } else {
            return false;
        }

    } catch (e) {
        return false;
    }
}

/*Return count of dates for given month and year.*/
function daysInMonth(iMonth, iYear) {
    return 32 - new Date(iYear, iMonth, 32).getDate();
}

/*Implement event listeners.*/
document.getElementById("next").addEventListener("click", await next);
document.getElementById("current").addEventListener("click", await currentDate);
document.getElementById("previous").addEventListener("click", await previous);
document.getElementById("month").addEventListener("change", await jump);
document.getElementById("year").addEventListener("change", await jump);