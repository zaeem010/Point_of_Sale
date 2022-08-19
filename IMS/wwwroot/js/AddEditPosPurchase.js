$(document).ready(function () {
    $("#PosPurchaseMaster_ThirdlevelId").select2({
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
                debugger;
                $('#Amount').val(data.purchasePrice);
                $('#Unit').val(data.unit);
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
    //
    //$('#PurchaseMaster_NetTotal').val(SubTotal)
    //let B = SubTotal - parseFloat(PaidTotal || 0);
    //$('#PurchaseMaster_BalanceTotal').val(B);
}
function deleteRow(btn) {
    if (confirm('Are you sure you want to delete this object?')) {
        $(btn).closest("tr").remove();
        let tota = 0;
        $(".totalprice").each(function () {
            tota += parseFloat($(this).text());
        });
        $('#PosPurchaseMaster_SubTotal').val(tota);
        //
        pricing();
        let type = 1;
        let message = "Deleted Successfully";
        ShowToaster(type, message);
    }
    return false;
}

var rowcount = $('#countofdetails').text();

$(document).keypress(function (e) {
    if (e.which == 13 || event.keyCode == 13) {
        //
        var selObj = document.getElementById("Product");
        var PName = selObj.options[selObj.selectedIndex].text;
        var selObj = document.getElementById("Product");
        var PId = selObj.options[selObj.selectedIndex].value;

        var Unit = $('#Unit').val();
        var Amount = $('#Amount').val();
        var Qty = $('#Qty').val();
        var total = $('#total').val();

        if (PId == "" || PId == 0)
        {
            focusproduct();
            let type = 0;
            let message = "Please Select Product";
            ShowToaster(type, message);
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
            htmlRows += '<tr >';
            htmlRows += '<td><a class="badge bg-danger" onclick="return deleteRow(this)" ><i class="bx bx-trash"></i></a></td>';
            htmlRows += '<td class="d-none">' + PId + '</td>';
            htmlRows += '<td>' + PName + '</td>';
            htmlRows += '<td>' + Unit + '</td>';
            htmlRows += '<td>' + Amount + '</td>';
            htmlRows += '<td>' + Qty + '</td>';
            htmlRows += '<td class="totalprice">' + total + '</td>';
            $('#Items').append(htmlRows);
            rowcount++;
            
            $('#Unit').val('');
            $('#Qty').val('1');
            $('#Amount').val('0');
            $('#total').val('0');
            focusproduct();
            let tota = 0;
            $(".totalprice").each(function () {
                tota += parseFloat($(this).text());
            });
            $('#PosPurchaseMaster_SubTotal').val(tota);
            //
            pricing();
            let type = 1;
            let message = "Added Successfully";
            ShowToaster(type, message);
        }
        //
    }
});
function submitform() {
    document.getElementById("PosPurchaseForm").submit();
}
function focusproduct() {
    $('#Product').select2({
        closeOnSelect: true,
        placeholder: "Select Product",
        width: 'resolve',
        height: 'resolve',
        theme: "classic"
    }).one('select2-focus', OpenSelect2)
        .on("select2-blur", function (e) {
            $(this).one('select2-focus', OpenSelect2)
        })

    function OpenSelect2() {
        var $select2 = $(this).data('select2');
        setTimeout(function () {
            if (!$select2.opened()) { $select2.open(); }
        }, 0);
    }
}

//$('#Adding').on('click', function () {
   

//});