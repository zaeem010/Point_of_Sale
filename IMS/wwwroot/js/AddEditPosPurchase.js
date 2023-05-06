$(document).ready(function () {
    $("#PosPurchaseMaster_ThirdLevelId").select2({
        closeOnSelect: true,
        placeholder: "Select Supplier",
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    });
    $("#Cat").select2({
        closeOnSelect: true,
        placeholder: "Select Category",
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    });
    $("#SubCat").select2({
        closeOnSelect: true,
        placeholder: "Select Sub Category",
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    });
    $("#Product").select2({
        closeOnSelect: true,
        placeholder: "Select Product",
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    });
    var Amount = document.getElementById('Amount');
    var Qty = document.getElementById('Qty');
    Amount.addEventListener('input', checkInputTel);
    Qty.addEventListener('input', checkInputTel);
});
function GetSubCategoryList(val) {
    GetProductByCatList(val)
    $.ajax({
        type: "GET",
        url: '/Pos/Product/GetSubCategoryList',
        data: { Id: val },
        success: function (data) {
            if (data != null) {
                debugger;
                var s = '<option value="">Select</option>';
                for (var i = 0; i < data.length; i++) {
                    s += '<option value="' + data[i].id + '">' + data[i].name + '</option>';
                }
                $('#SubCat').empty().append(s);
            }
        },
    });
};
function GetProductByCatList(val) {
    $.ajax({
        type: "GET",
        url: '/Pos/PosPurchase/GetProductByCatList',
        data: { Id: val },
        success: function (data) {
            if (data != null) {
                debugger;
                var s = '<option value="">Select</option>';
                for (var i = 0; i < data.length; i++) {
                    let sku = '';
                    if (data[i].sku != null)
                    {
                        sku = data[i].sku;
                    }
                    s += '<option value="' + data[i].id + '">' + data[i].name + ' || ' + sku + '</option>';
                }
                $('#Product').empty().append(s);
            }
        },
    });
};
function GetProductBysubList(val) {
    $.ajax({
        type: "GET",
        url: '/Pos/PosPurchase/GetProductBysubList',
        data: { Id: val },
        success: function (data) {
            if (data != null) {
                debugger;
                var s = '<option value="">Select</option>';
                for (var i = 0; i < data.length; i++) {
                    let sku = '';
                    if (data[i].sku != null) {
                        sku = data[i].sku;
                    }
                    s += '<option value="' + data[i].id + '">' + data[i].name + ' || ' + sku + '</option>';
                }
                $('#Product').empty().append(s);
            }
        },
    });
};
function GetProduct(val) {
    $.ajax({
        type: "GET",
        url: '/Pos/PosPurchase/GetProduct',
        data: { Id: val },
        success: function (data) {
            if (data != null) {
                $('#Amount').val(data.purchasePrice);
                $('#Unit').val(data.unit);
                $('#CategoryId').val(data.categoryId);
                $('#Qty').focus();
                pricing();
            }
        },
    });
};
function saveSupplier() {
    var ThirdLevelId = $('#PosPurchaseMaster_ThirdlevelId').val();
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
                $('#PosPurchaseMaster_ThirdlevelId').empty().append(s);
                $('#PosPurchaseMaster_ThirdlevelId').val(ThirdLevelId).trigger('change');
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
$(':input').on('keyup', function () {
    pricing();
});
function pricing() {
    let Amount = $('#Amount').val();
    let Qty = $('#Qty').val();
    let total = 0;
    //TotalPrice
    total = parseFloat(Amount || 0) * parseFloat(Qty || 0);
    $('#total').val(total);

    let tota = 0;
    $(".totalprice").each(function () {
        tota += parseFloat($(this).text());
    });
    $('#PosPurchaseMaster_NetTotal').val(tota);
    $('#PosPurchaseMaster_BalanceTotal').val(tota);
    const Ntotal = $('#PosPurchaseMaster_NetTotal').val();
    const PTotal = $('#PosPurchaseMaster_PaidTotal').val();
    const Balance = parseFloat(Ntotal || 0) - parseFloat(PTotal || 0);
    $('#PosPurchaseMaster_BalanceTotal').val(Balance);
    //

}
function deleteRow(btn) {
    if (confirm('Are you sure you want to delete this object?')) {
        $(btn).closest("tr").remove();
        //
        pricing();
        let type = 1;
        let message = "Deleted Successfully";
        ShowToaster(type, message);
    }
    return false;
}
function Editrow(button) {
    var $row = $(button).closest('tr');
    //
    const PId = $row.find("td:eq(1)").html();
    const PName = $row.find("td:eq(2)").html();
    const Unit = $row.find("td:eq(3)").html();
    const Amount = $row.find("td:eq(4)").html();
    const Qty = $row.find("td:eq(5)").html();
    const total = $row.find("td:eq(6)").html();
    const CategoryId = $row.find("td:eq(7)").html();

    $('#Product').val(PId).trigger("change");
    $('#Unit').val(Unit);
    $('#Qty').val(Qty);
    $('#Amount').val(Amount);
    $('#total').val(total);
    $('#CategoryId').val(CategoryId);
    //
    $(button).closest("tr").remove();
    pricing();
}

var rowcount = $('#countofdetails').text();
$(document).keypress(function (e) {
    if (e.which == 13 || e.keyCode == 13) {
        AddProduct();
    }
});
function AddProduct() {
    //
    var selObj = document.getElementById("Product");
    var PName = selObj.options[selObj.selectedIndex].text;
    var selObj = document.getElementById("Product");
    var PId = selObj.options[selObj.selectedIndex].value;

    var Unit = $('#Unit').val();
    var Amount = $('#Amount').val();
    var Qty = $('#Qty').val();
    var total = $('#total').val();
    var CategoryId = $('#CategoryId').val();

    if (PId == "" || PId == 0) {
        $("#Product").select2("open");
    }
    else if (Amount == "" || Amount == 0) {
        let type = 0;
        let message = "Please Enter Amount";
        ShowToaster(type, message);
    }
    else if (Qty == "" || Qty == 0) {
        let type = 0;
        let message = "Please Enter Qty";
        ShowToaster(type, message);
    }
    else {
        let htmlRows = '';
        htmlRows += '<tr data-id=' + PId + '>';
        htmlRows += '<td><a class="btn bg-danger" onclick="return deleteRow(this)" ><i class="bx bx-trash"></i></a><a class="btn bg-primary" onclick="return Editrow(this)" ><i class="bx bx-edit"></i></a></td>';
        htmlRows += '<td class="d-none">' + PId + '</td>';
        htmlRows += '<td>' + PName + '</td>';
        htmlRows += '<td>' + Unit + '</td>';
        htmlRows += '<td>' + Amount + '</td>';
        htmlRows += '<td>' + Qty + '</td>';
        htmlRows += '<td class="totalprice">' + total + '</td>';
        htmlRows += '<td class="d-none">' + CategoryId + '</td>';
        $('#Items').append(htmlRows);
        rowcount++;

        $('#Unit').val('');
        $('#Qty').val('1');
        $('#Amount').val('0');
        $('#total').val('0');
        $('#CategoryId').val('');
        //
        pricing();
        $('#Product').val(null).trigger("change");
        $("#Product").select2("open");
    }
        //
}

function submitform() {
    //PosPurchaseMaster
    let Vtype = "POSPINV";
    let Invid = $('#PosPurchaseMaster_Id').val();
    let InvDate = $('#PosPurchaseMaster_InvDate').val();
    let ThirdLevelId = $('#PosPurchaseMaster_ThirdLevelId').val();
    let Remarks = $('#PosPurchaseMaster_Remarks').val();
    let NetTotal = $('#PosPurchaseMaster_NetTotal').val();
    let PaidTotal = $('#PosPurchaseMaster_PaidTotal').val();
    //PosPurchaseDetail
    let PosPurchaseDetail = [];
    let Stock = [];
    $('#Items tbody tr').each((index, elem) => {
        $tr = $(elem);
        // check for empty row
        if ($tr.find('td').length > 1) {
            const details = {
                Id: 0,
                PosPurchaseMasterId: Invid,
                ProductId: $tr.find("td:eq(1)").text(),
                ProductName: $tr.find("td:eq(2)").text(),
                Unit: $tr.find("td:eq(3)").text(),
                PurchasePrice: $tr.find("td:eq(4)").text(),
                Qty: $tr.find("td:eq(5)").text(),
                Total: $tr.find("td:eq(6)").text(),
                CategoryId: $tr.find("td:eq(7)").text(),
            }
            const stockdetails = {
                Id: 0,
                CategoryId: $tr.find("td:eq(7)").text(),
                ProductId: $tr.find("td:eq(1)").text(),
                StockIn: $tr.find("td:eq(5)").text(),
                Date: InvDate,
                UserId: 2,
                PosPurchaseMasterId: Invid
            }
            PosPurchaseDetail.push(details);
            Stock.push(stockdetails);
            //
        }
    });
    //
    let PosPurchaseMaster = {
        Id: Invid,
        InvDate: InvDate,
        ThirdlevelId: ThirdLevelId,
        Remarks: Remarks,
        NetTotal: NetTotal,
        PaidTotal: PaidTotal,
        PosPurchaseDetailList: PosPurchaseDetail
    }
    //
    let TranscationDetails = [];
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
        HeadId: ThirdLevelId.charAt(0)
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
        HeadId:1
    }
    TranscationDetails.push(second)
    //
    if (PaidTotal > 0) {
        //
        const third = {
            Id: 0,
            Invid: Invid,
            Vtype: "CPVPOSPINV",
            TransDate: InvDate,
            ThirdLevelId: ThirdLevelId,
            Dr: PaidTotal,
            Cr: 0,
            UserId: 2,
            HeadId: ThirdLevelId.charAt(0)
        }
        TranscationDetails.push(third)
        //
        const fourth = {
            Id: 0,
            Invid: Invid,
            Vtype: "CPVPOSPINV",
            TransDate: InvDate,
            ThirdLevelId: "1100001",
            Dr: 0,
            Cr: PaidTotal,
            UserId: 2,
            HeadId: 1
        }
        TranscationDetails.push(fourth)
    }
    let PosPurchaseSavemodel = {
        PosPurchaseMaster: PosPurchaseMaster,
        TranscationDetailsList: TranscationDetails,
        StockList: Stock
    }
    debugger;
    //
    $.ajax({
        url: '/Pos/PosPurchase/Save',
        type: "post",
        data: PosPurchaseSavemodel,
        async: false,
        success: function (data) {
            if (data != null) {
                debugger;
                if (data.message == "Registerd Successfully") {
                    location.reload();
                }
                if (data.message == "Updated Successfully") {
                    window.location.replace("/Pos/Purchase/All-Purchases");
                }
            }
        }
    });
}
