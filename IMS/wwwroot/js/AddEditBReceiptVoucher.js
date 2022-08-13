$(document).ready(function () {
    $("#Account").select2({
        closeOnSelect: true,
        placeholder: "Select Account",
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    });
    $("#VoucherMaster_ThirdLevelId").select2({
        closeOnSelect: true,
        placeholder: "Select Bank",
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    });
    let Amount = document.getElementById('Amount');
    Amount.addEventListener('input', checkInputTel);

});
var userid = 2;
$('#Adding').on('click', function () {
    var selObj = document.getElementById("Account");
    var AccountName = selObj.options[selObj.selectedIndex].text;
    var selObj = document.getElementById("Account");
    var Account_No = selObj.options[selObj.selectedIndex].value;
    var Amount = $('#Amount').val();
    var Narration = $('#Narration').val();
    var Cledate = $('#Cledate').val();
    var Cheque = $('#Cheque').val();

    if (Account_No == "" || Account_No == 0) {
        let type = 0;
        let message = "Please Select Account";
        ShowToaster(type, message);
    }
    else if (Amount == "" || Amount == 0) {
        let type = 0;
        let message = "Please Enter Amount";
        ShowToaster(type, message);
    }
    else {
        let htmlRows = '';
        htmlRows += '<tr >';
        htmlRows += '<td><a class="badge bg-danger" onclick="return deleteRow(this)" ><i class="bx bx-trash"></i></a></td>';
        htmlRows += '<td class="d-none">' + Account_No + '</td>';
        htmlRows += '<td>' + AccountName + '</td>';
        htmlRows += '<td class="totalamount">' + Amount + '</td>';
        htmlRows += '<td>' + Cledate + '</td>';
        htmlRows += '<td>' + Cheque + '</td>';
        htmlRows += '<td>' + Narration + '</td>';
        $('#Items').append(htmlRows);

        $('#Account').val(null).trigger('change');
        $('#Amount').val('0');
        $('#Narration').val('');
        $('#Cheque').val('');
        $('#Cledate').val('');

        let tota = 0;
        $(".totalamount").each(function () {
            tota += parseFloat($(this).text());
        });
        $('#VoucherMaster_TAmount').val(tota);
        //
        let type = 1;
        let message = "Added Successfully";
        ShowToaster(type, message);
    }
});

function Save() {
    var selObj = document.getElementById("VoucherMaster_ThirdLevelId");
    var Bank_Name = selObj.options[selObj.selectedIndex].text;
    //
    let Vtype = "BRV";
    let VoucherDetail = [];
    let TranscationDetails = [];
    let Invid = $('#VoucherMaster_Id').val();
    let VoucherDate = $('#VoucherMaster_VoucherDate').val();
    let Remarks = $('#VoucherMaster_Remarks').val();
    let TAmount = $('#VoucherMaster_TAmount').val();
    let GeneralId = $('#VoucherMaster_GeneralId').val();
    let ThirdLevelId = $('#VoucherMaster_ThirdLevelId').val();
    let Mytransdesc = "(" + Bank_Name + ") Against Bank Receipt Voucher No " + GeneralId + " And Date was " + VoucherDate;
    //table data
    $('#Items tbody tr').each((index, elem) => {
        $tr = $(elem);
        // check for empty row
        if ($tr.find('td').length > 1) {
            const details = {
                Id: 0,
                VoucherMasterId: Invid,
                Amount: $tr.find("td:eq(3)").text(),
                Narration: $tr.find("td:eq(6)").text(),
                ThirdLevelId: $tr.find("td:eq(1)").text(),
                CleDate: $tr.find("td:eq(4)").text(),
                CheckNo: $tr.find("td:eq(5)").text(),
            }
            VoucherDetail.push(details);
            const trans = {
                Id: 0,
                Invid: Invid,
                Vtype: Vtype,
                TransDes: $tr.find("td:eq(6)").text(),
                TransDate: VoucherDate,
                ThirdLevelId: $tr.find("td:eq(1)").text(),
                Dr: $tr.find("td:eq(3)").text(),
                Cr: 0,
                UserId: userid,
                HeadId: $tr.find("td:eq(1)").text().charAt(0)
            }
            TranscationDetails.push(trans);
        }
    });

    //Transactions
    const first = {
        Id: 0,
        Invid: Invid,
        Vtype: Vtype,
        TransDes: Mytransdesc,
        TransDate: VoucherDate,
        ThirdLevelId: ThirdLevelId,
        Dr: 0,
        Cr: TAmount,
        UserId: userid,
        HeadId: ThirdLevelId.charAt(0)
    }
    TranscationDetails.push(first);
    let VoucherMaster = {
        Id: Invid,
        VoucherDate: VoucherDate,
        Remarks: Remarks,
        TAmount: TAmount,
        Vtype: Vtype,
        ThirdLevelId: ThirdLevelId,
        UserId: userid,
        GeneralId: GeneralId,
        VoucherDetail: VoucherDetail
    }
    let VoucherSavemodel = {
        VoucherMaster: VoucherMaster,
        TranscationDetailList: TranscationDetails
    };
    $.ajax({
        url: '/BankReceipt/Save',
        type: "post",
        data: VoucherSavemodel,
        async: false,
        success: function (data) {
            if (data != null) {
                if (data.message == "Registerd Successfully") {
                    location.reload();
                }
                if (data.message == "Updated Successfully") {
                    window.location.replace("/Bank-Receipt/Bank-Receive-Detail/");
                }
            }
        }
    });
}

function deleteRow(btn) {
    if (confirm('Are you sure you want to delete this object?')) {
        $(btn).closest("tr").remove();
        let tota = 0;
        $(".totalprice").each(function () {
            tota += parseFloat($(this).text());
        });
        $('#VoucherMaster_TAmount').val(tota);
        //
        let type = 1;
        let message = "Deleted Successfully";
        ShowToaster(type, message);
    }
    return false;
}