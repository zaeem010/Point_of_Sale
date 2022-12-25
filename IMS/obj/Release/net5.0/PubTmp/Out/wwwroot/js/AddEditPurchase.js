$(document).ready(function () {
    $("#PurchaseMaster_ThirdLevelId").select2({
        closeOnSelect: true,
        placeholder: "Select Supplier",
        //allowHtml: true,
        //allowClear: true,
        //tags: true // создает новые опции на лету
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    });
    var VRate = document.getElementById('VRate');
    var VCargo = document.getElementById('VCargo');
    var VDis = document.getElementById('VDis');
    VRate.addEventListener('input', checkInputTel);
    VCargo.addEventListener('input', checkInputTel);
    VDis.addEventListener('input', checkInputTel);
});
function saveSupplier()
{
    var ThirdLevelId = $('#PurchaseMaster_ThirdLevelId').val();
    var Name = $('#Supplier_Name').val();
    var Company = $('#Supplier_Company').val();
    var Address = $('#Supplier_Address').val();
    var BanckName = $('#Supplier_BanckName').val();
    var AccountNo = $('#Supplier_AccountNo').val();
    var AccountHolder = $('#Supplier_AccountHolder').val();
    var NTN = $('#Supplier_NTN').val();
    var Gst = $('#Supplier_Gst').val();
    var SpDis = $('#Supplier_SpDis').val();
    var Verify = "true";
    let Supplier = {
        Name: Name,
        Company: Company,
        Address: Address,
        BanckName: BanckName,
        AccountNo: AccountNo,
        AccountHolder: AccountHolder,
        NTN: NTN,
        Gst: Gst,
        SpDis: SpDis,
        Verify: Verify,
    }
    $.ajax({
        url: '/Purchase/SaveSupplier',
        type: "post",
        data: Supplier,
        async: false,
        success: function (data) {
            if (data != null) {
                var s = '';
                for (var i = 0; i < data.supplierList.length; i++) {
                    s += '<option value="' + data.supplierList[i].thirdLevelId + '">' + data.supplierList[i].name + '</option>';
                }
                $('#PurchaseMaster_ThirdLevelId').empty().append(s);
                $('#PurchaseMaster_ThirdLevelId').val(ThirdLevelId).trigger('change');
            }
        }
    });
    let type = "success";
    let message = 'Registered Successfully';
    ShowToaster(type, message);

    $('#Supplier_Name').val('');
    $('#Supplier_Company').val('');
    $('#Supplier_Address').val('');
    $('#Supplier_BanckName').val('');
    $('#Supplier_AccountNo').val('');
    $('#Supplier_AccountHolder').val('');
    $('#Supplier_NTN').val('');
    $('#Supplier_Gst').val('');
    $('#Supplier_SpDis').val('');
    $('#AddSupplier').modal("toggle");
}
function saveVehicle()
{
    var Name = $('#ModelNo_Name').val();
    let ModelNo = {
        Name:Name
    }
    $.ajax({
        url: '/ModelNo/SaveVehicle',
        type: "post",
        data: ModelNo,
        async: false,
        success: function (data) {
            if (data != null) {
                var s = '<option value="">Select</option>';
                for (var i = 0; i < data.li.length; i++) {
                    s += '<option value="' + data.li[i].name + '">' + data.li[i].name + '</option>';
                }
                $('#VName').empty().append(s);
                $('#VName').val(null).trigger('change');
            }
        }
    });
    let type = "success";
    let message = 'Registered Successfully';
    ShowToaster(type, message);
    $('#ModelNo_Name').val('');
    $('#AddName').modal("toggle");
}

$(':input').on('keyup', function () {
    pricing();
});
function pricing() {
    let VRate = $('#VRate').val();
    let VCargo = $('#VCargo').val();
    let VDis = $('#VDis').val();
    let SubTotal = $('#PurchaseMaster_SubTotal').val();
    let CargoTotal = $('#PurchaseMaster_CargoTotal').val();
    let PaidTotal = $('#PurchaseMaster_PaidTotal').val();
    let Vtotal = 0;
    //amprice
    Vtotal = (parseFloat(VRate || 0) + parseFloat(VCargo || 0)) - parseFloat(VDis || 0);
    $('#Vtotal').val(Vtotal);
    //
    $('#PurchaseMaster_NetTotal').val(SubTotal)
    let B = SubTotal - parseFloat(PaidTotal || 0);
    $('#PurchaseMaster_BalanceTotal').val(B);
}

var rowcount = $('#countofdetails').text();
$('#Adding').on('click', function () {

    var VName = $('#VName').val();
    var VModel = $('#VModel').val();
    var VChassis = $('#VChassis').val();
    var VEngine = $('#VEngine').val();
    var VKey = $('#VKey').val();
    var VColor = $('#VColor').val();
    var VBikeNo = $('#VBikeNo').val();
    var VRate = $('#VRate').val();
    var VCargo = $('#VCargo').val();
    var VDis = $('#VDis').val();
    var Vtotal = $('#Vtotal').val();

    if (VName == "" || VName == 0) {
        let type = 0;
        let message = "Please Select Vehicle Name";
        ShowToaster(type, message);
    } else if (VModel == "" || VModel == 0) {
        let type = 0;
        let message = "Please Select Model";
        ShowToaster(type, message);
    }
    else if (VChassis == "" || VChassis == 0) {
        let type = 0;
        let message = "Please Enter Chassis";
        ShowToaster(type, message);
    }
    else if (VEngine == "" || VEngine == 0) {
        let type = 0;
        let message = "Please Enter Engine";
        ShowToaster(type, message);
    }
    else if (VKey == "" || VKey == 0) {
        let type = 0;
        let message = "Please Enter Key";
        ShowToaster(type, message);
    } else if (VColor == "" || VColor == 0) {
        let type = 0;
        let message = "Please Select Color";
        ShowToaster(type, message);
    }
    else if (VRate == "" || VRate == 0) {
        let type = 0;
        let message = "Please Enter Rate";
        ShowToaster(type, message);
    }
    else {
        let htmlRows = '';
        htmlRows += '<tr >';
        htmlRows += '<td><a class="badge bg-danger" onclick="return deleteRow(this)" ><i class="bx bx-trash"></i></a></td>';
        htmlRows += '<td>' + VName + '</td>';
        htmlRows += '<td>' + VModel + '</td>';
        htmlRows += '<td>' + VChassis +'</td>';
        htmlRows += '<td>' + VEngine + '</td>';
        htmlRows += '<td>' + VKey + '</td>';
        htmlRows += '<td>' + VColor + '</td>';
        htmlRows += '<td>' + VBikeNo + '</td>';
        htmlRows += '<td>' + VRate + '</td>';
        htmlRows += '<td class="totalcargo">' + VCargo + '</td>';
        htmlRows += '<td>' + VDis + '</td>';
        htmlRows += '<td class="totalprice">' + Vtotal + '</td>';
        $('#Items').append(htmlRows);
        rowcount++;
        $('#VName').val('');
        $('#VModel').val('');
        $('#VChassis').val('');
        $('#VEngine').val('');
        $('#VKey').val('');
        $('#VColor').val('Red');
        $('#VBikeNo').val('New');
        $('#VRate').val('0');
        $('#VCargo').val('0');
        $('#VDis').val('0');
        $('#Vtotal').val('0');

        let tota = 0;
        let totacargo = 0;
        $(".totalprice").each(function () {
            tota += parseFloat($(this).text());
        });
        $(".totalcargo").each(function () {
            totacargo += parseFloat($(this).text());
        });
        $('#PurchaseMaster_SubTotal').val(tota);
        $('#PurchaseMaster_CargoTotal').val(totacargo);
        //
        pricing();
        let type = 1;
        let message = "Added Successfully";
        ShowToaster(type, message);
        //$('#Product').val(null).trigger('change');
    }

});

function deleteRow(btn) {
    if (confirm('Are you sure you want to delete this object?')) {
        $(btn).closest("tr").remove();
        let tota = 0;
        let totacargo = 0;
        $(".totalprice").each(function () {
            tota += parseFloat($(this).text());
        });
        $(".totalcargo").each(function () {
            totacargo += parseFloat($(this).text());
        });
        $('#PurchaseMaster_SubTotal').val(tota);
        $('#PurchaseMaster_CargoTotal').val(totacargo);
        //
        pricing();
        let type = 1;
        let message = "Deleted Successfully";
        ShowToaster(type, message);
    }
    return false;
}


function Save()
{
    let PurchaseDetail = [];
    let TranscationDetails = [];
    let Invid = $('#PurchaseMaster_Id').val();
    let InvDate = $('#PurchaseMaster_InvDate').val();
    let ThirdLevelId = $('#PurchaseMaster_ThirdLevelId').val();
    let CargoId = $('#PurchaseMaster_CargoId').val();
    let Remarks = $('#PurchaseMaster_Remarks').val();
    let SubTotal = $('#PurchaseMaster_SubTotal').val();
    let CargoTotal = $('#PurchaseMaster_CargoTotal').val();
    let NetTotal = $('#PurchaseMaster_NetTotal').val();
    let PaidTotal = $('#PurchaseMaster_PaidTotal').val();
    let BalanceTotal = $('#PurchaseMaster_BalanceTotal').val();
    

    //table interiorDesignergrouptable data
    $('#Items tbody tr').each((index, elem) => {
        $tr = $(elem);
        // check for empty row
        if ($tr.find('td').length > 1) {
            const details = {
                Id: 0,
                PurchaseMasterId: Invid,
                VName: $tr.find("td:eq(1)").text(),
                ModelNo: $tr.find("td:eq(2)").text(),
                ChassisNo: $tr.find("td:eq(3)").text(),
                EngineNo: $tr.find("td:eq(4)").text(),
                KeyNo: $tr.find("td:eq(5)").text(),
                BikeNo: $tr.find("td:eq(6)").text(),
                Color: $tr.find("td:eq(7)").text(),
                Rate: $tr.find("td:eq(8)").text(),
                CargoRate: $tr.find("td:eq(9)").text(),
                Discount: $tr.find("td:eq(10)").text(),
                Total: $tr.find("td:eq(11)").text(),
            }
            PurchaseDetail.push(details);
        }
    });
    let Vtype = "PINV";
    //Transactions
    const first = {
        Id: 0,
        Invid: Invid,
        Vtype: Vtype,
        TransDate: InvDate,
        ThirdLevelId: ThirdLevelId,
        Dr: 0,
        Cr: NetTotal,
        UserId: 2,
    }
    TranscationDetails.push(first)
    //
    const second = {
        Id: 0,
        Invid: Invid,
        Vtype: Vtype,
        TransDate: InvDate,
        ThirdLevelId: "1100002",
        Dr: NetTotal,
        Cr: 0,
        UserId: 2,
    }
    TranscationDetails.push(second)
    //
    if (PaidTotal > 0)
    {
        //
        const third = {
            Id: 0,
            Invid: Invid,
            Vtype: "CPVPINV",
            TransDate: InvDate,
            ThirdLevelId: ThirdLevelId,
            Dr: PaidTotal,
            Cr: 0,
            UserId: 2,
        }
        TranscationDetails.push(third)
        //
        const fourth = {
            Id: 0,
            Invid: Invid,
            Vtype: "CPVPINV",
            TransDate: InvDate,
            ThirdLevelId: "1100001",
            Dr: 0,
            Cr: PaidTotal,
            UserId: 2,
        }
        TranscationDetails.push(fourth)
    }

    let PurchaseMaster = {
        Id: Invid,
        InvDate: InvDate,
        ThirdLevelId: ThirdLevelId,
        CargoId: CargoId,
        Remarks: Remarks,
        SubTotal: SubTotal,
        CargoTotal: CargoTotal,
        NetTotal: NetTotal,
        PaidTotal: PaidTotal,
        BalanceTotal: BalanceTotal,
        PurchaseDetailList: PurchaseDetail,
    };
    let purchaseSavemodel = {
        PurchaseMaster: PurchaseMaster,
        TranscationDetailList: TranscationDetails
    };
    $.ajax({
        url: '/Purchase/Save',
        type: "post",
        data: purchaseSavemodel,
        async: false,
        success: function (data) {
            if (data != null) {
                debugger;
                if (data.message == "Registerd Successfully")
                {
                    location.reload();
                }
                if (data.message == "Updated Successfully")
                {
                    window.location.replace("/Purchase/All-Purchases/");
                }
            }
        }
    });
}












//htmlRows += '<td><a class="badge bg-danger" onclick="return deleteRow(this)" ><i class="bx bx-trash"></i></a></td>';
//htmlRows += '<td>' + VName + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].VName" value="' + VName + '" /></td>';
//htmlRows += '<td>' + VModel + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].ModelNo" value="' + VModel + '" /></td>';
//htmlRows += '<td>' + VChassis + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].ChassisNo" value="' + VChassis + '" /></td>';
//htmlRows += '<td>' + VEngine + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].EngineNo" value="' + VEngine + '" /></td>';
//htmlRows += '<td>' + VKey + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].KeyNo" value="' + VKey + '" /></td>';
//htmlRows += '<td>' + VColor + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].Color" value="' + VColor + '" /></td>';
//htmlRows += '<td>' + VBikeNo + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].BikeNo" value="' + VBikeNo + '" /></td>';
//htmlRows += '<td>' + VRate + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].Rate" value="' + VRate + '" /></td>';
//htmlRows += '<td>' + VCargo + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].CargoRate" value="' + VCargo + '" /></td>';
//htmlRows += '<td>' + VDis + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].Discount" value="' + VDis + '" /></td>';
//htmlRows += '<td>' + Vtotal + ' <input type="hidden" name="PurchaseDetailList[' + rowcount + '].Total" value="' + Vtotal + '" /></td>';
//htmlRows += '<td class="d-none totalprice">' + Vtotal + '</td>';