function Getproducts(code) {
    $("#filteredby").empty();
    $.ajax({
        url: '/Pos/Sale/GetProductbyCatId',
        type: "GET",
        data: { "Id": code },
        success: function (data) {
            $(".cc").empty();
            var producthtmlstring = returnproducts(data);
            $('.cc').html(producthtmlstring);
            $('.cc').html(s);
        }
    });
}
$(document).ready(function (e) {
    $('#seachbyname').keyup(delay(function (e) {
        let searchterm = this.value;
        if (searchterm == "") {
            GetAllProducts();
        }
        $.ajax({
            url: '/Pos/Sale/GetProductbyName',
            type: "GET",
            data: { "searchterm": searchterm },
            success: function (data) {
                $(".cc").empty();
                $("#filteredby").text(searchterm);
                var producthtmlstring = returnproducts(data);
                $('.cc').html(producthtmlstring);
            }
        });
    }, 500));
    //
    $('#seachbyprice').keyup(delay(function (e) {
        let searchterm = parseInt(this.value) || 0;
        if (searchterm == 0) {
            GetAllProducts();
        }
        $.ajax({
            url: '/Pos/Sale/GetProductbyPrice',
            type: "GET",
            data: { "searchterm": searchterm },
            success: function (data) {
                $(".cc").empty();
                $("#filteredby").text(searchterm);
                var producthtmlstring = returnproducts(data);
                $('.cc').html(producthtmlstring);
            }
        });
    }, 500));
});

function GetAllProducts() {
    $.ajax({
        url: '/Pos/Sale/GetAllProduct',
        type: "GET",
        success: function (data) {
            $(".cc").empty();
            var producthtmlstring = returnproducts(data);
            $('.cc').html(producthtmlstring);
        }
    });
}
function returnproducts(data)
{
    var producthtmlstring = '';
    if (data.length != 0) {
        for (var i = 0; i < data.length; i++) {
            let imgpath = "/no-image.png";
            if (data[i].imagePath != null) {
                imgpath = `/Uploads/${data[i].imagePath}`;
            }
            producthtmlstring += `<div class="col-xl-3">
                                    <div class="productCard">
                                        <a onclick="return add_tocart(${data[i].id})">
                                            <div class="productThumb Product-container">
                                                <img class="img-fluid" src="${imgpath}">
                                                <div class="bottom-right"><b>St:${data[i].currentStock}</b></div>
                                            </div>
                                            <div class="productContent text-center">
                                                ${data[i].name} <b>Sp:${data[i].salePrice}</b>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="btn-group">
                                        <button type="button" onclick="Addtocart(this);" class="btn btn-primary" categoryId="${data[i].categoryId}" currentstock="${data[i].currentStock}" productname="${data[i].name}" saleprice="${data[i].salePrice}" value="2" id="${data[i].id}">2</button>
                                        <button type="button" onclick="Addtocart(this);" class="btn btn-primary" categoryId="${data[i].categoryId}" currentstock="${data[i].currentStock}" productname="${data[i].name}" saleprice="${data[i].salePrice}" value="3" id="${data[i].id}">3</button>
                                        <button type="button" onclick="Addtocart(this);" class="btn btn-primary" categoryId="${data[i].categoryId}" currentstock="${data[i].currentStock}" productname="${data[i].name}" saleprice="${data[i].salePrice}" value="4" id="${data[i].id}">4</button>
                                        <button type="button" onclick="Addtocart(this);" class="btn btn-primary" categoryId="${data[i].categoryId}" currentstock="${data[i].currentStock}" productname="${data[i].name}" saleprice="${data[i].salePrice}" value="5" id="${data[i].id}">5</button>
                                    </div>
                                    <div class="btn-group">
                                        <input type="number" id="customval_${data[i].id}" class="form-control" value="1" style="background:white;height:45px" autofocus />
                                        <button type="button" onclick="AddtocartMan(this);" class="btn btn-primary" categoryId="${data[i].categoryId}" currentstock="${data[i].currentStock}" productname="${data[i].name}" saleprice="${data[i].salePrice}" id="${data[i].id}"><i class="fa fa-check"></i></button>
                                    </div>
                                </div>`;
        }
    } else {
        producthtmlstring = `<div class="col-md-12 text-center">
                                <h3 class="text-danger">No record Found!</h3>
                            </div>`;
    }
    return producthtmlstring;
}

//Add to cart with manual qty
function AddtocartMan(val) {
    let ProId = val.id;
    let Qty = parseInt($('#customval_' + ProId).val());
    let saleprice = parseInt(val.getAttribute("saleprice"));
    let productname = val.getAttribute("productname");
    let stock = parseInt(val.getAttribute("currentstock"));
    let categoryId = val.getAttribute("categoryId");
    //
    var row = $('table#invoiceItem').find(`#row_${ProId}`);
    let newQty = row.find("td:eq(2)").text();
    if (newQty != "") {
        Qty = parseInt(newQty) + Qty;
    }
    let htmlstring = '';
    if (Qty <= stock) {
        if (row.length == 0) {
            htmlstring = `<tr id="row_${ProId}">
                <td>${productname}</td>
                <td>${saleprice}</td>
                <td>${Qty}</td>
                <td>${saleprice * Qty}</td>
                <td class="d-none">${ProId}</td>
                <td class="d-none">${categoryId}</td>
                <td style="width:40px !important"><a  class="btn btn-danger deleterow" style="background:red"><i class="fa fa-trash"></i></a></td>
            </tr>`;
        } else {

            row.remove();
            htmlstring = `<tr id="row_${ProId}">
                <td>${productname}</td>
                <td>${saleprice}</td>
                <td>${Qty}</td>
                <td>${saleprice * Qty}</td>
                <td class="d-none">${ProId}</td>
                <td class="d-none">${categoryId}</td>
                <td style="width:40px !important"><a  class="btn btn-danger deleterow" style="background:red"><i class="fa fa-trash"></i></a></td>
            </tr>`;
        }

    } else {
        alert("out of Stock.");
    }
    $('#invoiceItemrows').append(htmlstring);
    //
    calculatetotal();
}
//Add to cart default
function Addtocart(val)
{
    let ProId = val.id;
    let Qty = parseInt(val.value);
    let saleprice = parseInt( val.getAttribute("saleprice"));
    let productname = val.getAttribute("productname");
    let stock = parseInt( val.getAttribute("currentstock"));
    let categoryId = val.getAttribute("categoryId");
    //
    var row = $('table#invoiceItem').find(`#row_${ProId}`);
    let newQty = row.find("td:eq(2)").text() ;
    if (newQty != "") {
        Qty = parseInt(newQty) + Qty;
    }
    let htmlstring = '';
    if (Qty <= stock) {
        if (row.length == 0) {
            htmlstring = `<tr id="row_${ProId}">
                <td>${productname}</td>
                <td>${saleprice}</td>
                <td>${Qty}</td>
                <td>${saleprice * Qty}</td>
                <td class="d-none">${ProId}</td>
                <td class="d-none">${categoryId}</td>
                <td style="width:40px !important"><a  class="btn btn-danger deleterow" style="background:red"><i class="fa fa-trash"></i></a></td>
            </tr>`;
        } else {
            
            row.remove();
            htmlstring = `<tr id="row_${ProId}">
                <td>${productname}</td>
                <td>${saleprice}</td>
                <td>${Qty}</td>
                <td>${saleprice * Qty}</td>
                <td class="d-none">${ProId}</td>
                <td class="d-none">${categoryId}</td>
                <td style="width:40px !important"><a  class="btn btn-danger deleterow" style="background:red"><i class="fa fa-trash"></i></a></td>
            </tr>`;
        }
       
    } else {
        alert("out of Stock.");
    }
    $('#invoiceItemrows').append(htmlstring);
    //
    calculatetotal();
}

function calculatetotal() {
    var table = document.getElementById("invoiceItemrows");
    let sumVal = 0;
    if (table.rows.length != 0) {
        for (var i = 0; i < table.rows.length; i++) {
            sumVal = sumVal + parseInt(table.rows[i].cells[3].innerText);
        }
    }
    document.getElementById("Total").value = sumVal;
    document.getElementById("GTotal").value = sumVal;
    document.getElementById("RTotal").value = sumVal;
}
$(document).on('click', '.deleterow', e => {
    $(e.target).closest('tr').remove();
    calculatetotal();
});
function checkoutcalculations()
{
    let total = $('#Total').val();
    let Dis = $('#Dis').val();
    let GTotal = $('#GTotal').val();
    let RTotal = $('#RTotal').val();
    //
    let gt = total - Dis;
    let rt = GTotal - RTotal;

    $('#GTotal').val(gt)
    $('#RTotal').val(gt)
    $('#BTotal').val(rt)
}
function checkoutcalculation()
{
    let GTotal = $('#GTotal').val();
    let RTotal = $('#RTotal').val();
    //
    let rt = RTotal - GTotal;
    $('#BTotal').val(rt)
}

$(function () {
    $('#Dis').keyup(delay(function (e) {
        checkoutcalculations();
    }, 100));
    $('#RTotal').keyup(delay(function (e) {
        checkoutcalculation();
    }, 100));
});

function saveinvoice()
{
    let ThirdLevelId = "1100003";
    let Vtype = "POSSINV";
    let Remarks = "General Sale";
    let InvDate = $('#InvDate').val();
    let Amount = $('#GTotal').val();
    let Receive = $('#RTotal').val();
    let Invid = 0;
    //let Mytransdesc = "(Cash) Against Cash Receipt Voucher No " + GeneralId + " And Date was " + VoucherDate;
    let PosSaleDetail = [];
    let Stock = [];
    let TranscationDetails = [];
    $('#invoiceItem tbody tr').each((index, elem) => {
        $tr = $(elem);
        debugger;
        // check for empty row
        if ($tr.find('td').length > 1) {
            const details = {
                Id: 0,
                PosSaleMasterId: 0,
                CategoryId: $tr.find("td:eq(5)").text(),
                ProductId: $tr.find("td:eq(4)").text(),
                ProductName: $tr.find("td:eq(0)").text(),
                Unit: 'none',
                Qty: $tr.find("td:eq(2)").text(),
                SalePrice: $tr.find("td:eq(1)").text(),
                Total: $tr.find("td:eq(3)").text(),
            }
            const stockdetails = {
                Id: 0,
                CategoryId: $tr.find("td:eq(5)").text(),
                ProductId: $tr.find("td:eq(4)").text(),
                StockOut: $tr.find("td:eq(2)").text(),
                Date: InvDate,
                UserId: 2,
                PosMasterId: 0
            }
            PosSaleDetail.push(details);
            Stock.push(stockdetails);
            //
        }
    });
    //Transactions
    const first = {
        Id: 0,
        Invid: Invid,
        Vtype: Vtype,
        TransDate: InvDate,
        ThirdLevelId: ThirdLevelId,
        Dr: Amount,
        Cr: 0,
        UserId: 2,
        HeadId: ThirdLevelId.charAt(0)
    }
    TranscationDetails.push(first);
    //
    const second = {
        Id: 0,
        Invid: Invid,
        Vtype: Vtype,
        TransDate: InvDate,
        ThirdLevelId: "4400001",
        Dr: 0,
        Cr: Amount,
        UserId: 2,
        HeadId: ThirdLevelId.charAt(0)
    }
    TranscationDetails.push(second);
    //
    const third = {
        Id: 0,
        Invid: Invid,
        Vtype: Vtype,
        TransDate: InvDate,
        ThirdLevelId: "1100002",
        Dr: 0,
        Cr: Amount,
        UserId: 2,
        HeadId: ThirdLevelId.charAt(0)
    }
    TranscationDetails.push(third);
    //
    const fourth = {
        Id: 0,
        Invid: Invid,
        Vtype: Vtype,
        TransDate: InvDate,
        ThirdLevelId: "5500001",
        Dr: Amount,
        Cr: 0,
        UserId: 2,
        HeadId: ThirdLevelId.charAt(0)
    }
    TranscationDetails.push(fourth);
    //
    if (Receive > 0) {
        //
        const fifth = {
            Id: 0,
            Invid: Invid,
            Vtype: "CRVPOSSINV",
            TransDate: InvDate,
            ThirdLevelId: "1100001",
            Dr: Amount,
            Cr: 0,
            UserId: 2,
            HeadId: ThirdLevelId.charAt(0)
        }
        TranscationDetails.push(fifth);
        //
        const sixth = {
            Id: 0,
            Invid: Invid,
            Vtype: "CRVPOSSINV",
            TransDate: InvDate,
            ThirdLevelId: ThirdLevelId,
            Dr: 0,
            Cr: Amount,
            UserId: 2,
            HeadId: ThirdLevelId.charAt(0)
        }
        TranscationDetails.push(sixth);
    }
    let PosSaleMaster = {
        Id: 0,
        InvDate: InvDate,
        ThirdLevelId: ThirdLevelId,
        NetTotal: $('#Total').val(),
        Discount: $('#Dis').val(),
        GrandTotal: $('#GTotal').val(),
        ReceivedTotal: $('#RTotal').val(),
        ReturnPrice: $('#BTotal').val(),
        Remarks: Remarks,
        VType: Vtype,
        Status: 'Sale_Screen_1',
        PosSaleDetails: PosSaleDetail
    };
    let PosSaleSavemodel = {
        PosSaleMaster: PosSaleMaster,
        TranscationDetailsList: TranscationDetails,
        StockList: Stock
    };
    debugger;
    //
    $.ajax({
        url: '/Pos/Sale/Save',
        type: "post",
        data: PosSaleSavemodel,
        async: false,
        success: function (data) {
            if (data != null) {
                debugger;
                if (data.message == "Registerd Successfully") {
                    location.reload();
                }
                //if (data.message == "Updated Successfully") {
                //    window.location.replace("/Pos/Purchase/All-Purchases");
                //}
            }
        }
    });
}

