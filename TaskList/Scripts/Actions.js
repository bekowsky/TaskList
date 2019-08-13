

function ShowDates(max, day) {
    var i, j;
    var days = 1;
    for (i = 1; i < 7; i++) {
        var str= '';
        for (j = 1; (j < 8) && (days < max+1); j++) {
            if ((i == 1) && (j < day))
                str += '<td></td>'; else {
                str += '<td><button type="button" class="btn btn-secondary btn-lg">' + days + '</button></td>';
                days++;
            }
        }
        var hui = 'str' + i;
        var elem = document.getElementById(hui);
        elem.innerHTML = str;

    }
 
}

function Prev(Projects) {
   var prj = Projects;
}




window.onload = function () {
    ShowDates(31, 4);
    $("#LayoutCalendar")
        .parent()
        .addClass("active");
};