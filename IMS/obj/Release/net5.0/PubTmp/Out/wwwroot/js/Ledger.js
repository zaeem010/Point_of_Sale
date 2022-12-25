$(document).ready(function () {
    //Today Start
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    today = yyyy + '-' + mm + '-' + dd;
    var EnDate = "2022-01-01";
    document.getElementById("StDate").setAttribute("max", today);
    document.getElementById("StDate").setAttribute("min", EnDate);
    //
    endatechange();
})
function endatechange() {
    let selectedstartdate = $('#StDate').val();
    let enddate= new Date(selectedstartdate);
    enddate.setDate(enddate.getDate() + 120);
    //
    var pdd = enddate.getDate();
    var pmm = enddate.getMonth() + 1; //January is 0!
    var pyyyy = enddate.getFullYear();
    if (pdd < 10) {
        pdd = '0' + pdd
    }
    if (pmm < 10) {
        pmm = '0' + pmm
    }
    enddate = pyyyy + '-' + pmm + '-' + pdd;
    //
    document.getElementById("EnDate").setAttribute("min", selectedstartdate);
    document.getElementById("EnDate").setAttribute("max", enddate);
}

