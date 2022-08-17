// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#example').DataTable();

    $("#LedgerAccountNo").select2({
        closeOnSelect: true,
        placeholder: "Select Account",
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    });
    $("#CategoryId").select2({
        closeOnSelect: true,
        placeholder: "Select Category",
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    });

    //$('.datepicker').flatpickr({
    //    altInput: true,
    //    altFormat: "J F, Y",
    //    dateFormat: "Y-m-d",
    //    minDate: maxLimitDate,
    //    maxDate: new Date().fp_incr(0),
    //});
});
$(function () {
	$('[data-bs-toggle="popover"]').popover();
	$('[data-bs-toggle="tooltip"]').tooltip();
})

function notEmpty(value) {
    return (typeof value !== 'undefined' && value.trim().length);
}
//
function ShowToaster(type, text) {
    let toasterType;
    let title;
    switch (type) {
        case 1:
        default:
            toasterType = "success";
            title = "Success";
            break;
        case 0:
            toasterType = "error";
            title = "Error!";
            break;
    }
    Lobibox.notify(toasterType, {
        pauseDelayOnHover: true,
        continueDelayOnInactiveTab: false,
        position: 'top right',
        showClass: 'rollIn',
        hideClass: 'rollOut',
        icon: 'bx bx-check-circle',
        width: 600,
        msg: text
    });
}
//.

//
var goodKey = '0123456789. ';

var checkInputTel = function (e) {
    var key = (typeof e.which == "number") ? e.which : e.keyCode;
    var start = this.selectionStart,
        end = this.selectionEnd;

    var filtered = this.value.split('').filter(filterInput);
    this.value = filtered.join("");

    /* Prevents moving the pointer for a bad character */
    var move = (filterInput(String.fromCharCode(key)) || (key == 0 || key == 8)) ? 0 : 1;
    this.setSelectionRange(start - move, end - move);
}

var filterInput = function (val) {
    return (goodKey.indexOf(val) > -1);
}
