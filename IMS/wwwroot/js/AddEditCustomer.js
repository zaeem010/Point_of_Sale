$(document).ready(function () {
    $('#Gurantor_Cnic').inputmask("99999-9999999-9");
    $('#Customer_Cnic').inputmask("99999-9999999-9");

    $('#Customer_ContactNo').inputmask('0399-9999999');
    $('#Gurantor_ContactNo').inputmask('0399-9999999');
    $("#GurantorLi").select2({
        closeOnSelect: false,
        placeholder: "Select",
        allowHtml: true,
        //allowClear: true,
        //tags: true // создает новые опции на лету
    });
});

function CninFront(event) {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('Front');
        output.src = reader.result;
    }
    reader.readAsDataURL(event.target.files[0]);
};
function CninBack(event) {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('Back');
        output.src = reader.result;
    }
    reader.readAsDataURL(event.target.files[0]);
};
function Userimg(event) {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('Img');
        output.src = reader.result;
    }
    reader.readAsDataURL(event.target.files[0]);
};

$(document).on('change', '#Gurantor_CnicFront', function () {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('AFront');
        output.src = reader.result;
    }
    reader.readAsDataURL(event.target.files[0]);
})

$(document).on('change', '#Gurantor_CnicBack', function () {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('ABack');
        output.src = reader.result;
    }
    reader.readAsDataURL(event.target.files[0]);
});

$(document).on('change', '#Gurantor_ImageUrl', function () {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('AImg');
        output.src = reader.result;
    }
    reader.readAsDataURL(event.target.files[0]);
});
function Save()
{
    var Name = $('#Gurantor_Name').val();
    var FatherName = $('#Gurantor_FatherName').val();
    var ContactNo = $('#Gurantor_ContactNo').val();
    var Cnic = $('#Gurantor_Cnic').val();
    var ChequeNo = $('#Gurantor_ChequeNo').val();

    var filesData = new FormData();
    if (document.getElementById("Gurantor_CnicFront").files[0] != undefined) {
        const AFront = document.getElementById("Gurantor_CnicFront").files[0];
        filesData.append("Front", AFront);
    }
    if (document.getElementById("Gurantor_CnicBack").files[0] != undefined) {
        const ABack = document.getElementById("Gurantor_CnicBack").files[0];
        filesData.append("Back", ABack);
    }
    if (document.getElementById("Gurantor_ImageUrl").files[0] != undefined) {
        const AImg = document.getElementById("Gurantor_ImageUrl").files[0];
        filesData.append("Img", AImg);
    }
    var Gurantor = {
        Name: Name,
        FatherName: FatherName,
        ContactNo: ContactNo,
        Cnic: Cnic,
        ChequeNo: ChequeNo,
        Verify: "true",
    }
    filesData.append("CustomerRequest", JSON.stringify(Gurantor));
    $.ajax({
        url: '/Customer/SaveGurantor',
        type: "post",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: filesData,
        dataType: "json",
        success: function (data) {
            if (data != null) {
                var s = '';
                for (var i = 0; i < data.gurantorList.length; i++) {
                    s += '<option value="' + data.gurantorList[i].id + '">' + data.gurantorList[i].name + '</option>';
                }
                $('#GurantorLi').empty().append(s);
                $('#GurantorLi').val(null).trigger('change');
            }
        }
    });
    $('#Gurantor_Name').val('');
    $('#Gurantor_FatherName').val('');
    $('#Gurantor_ContactNo').val('');
    $('#Gurantor_Cnic').val('');
    $('#Gurantor_ChequeNo').val('');
    $('#Gurantor_CnicFront').val('');
    $('#Gurantor_CnicBack').val('');
    $('#Gurantor_ImageUrl').val('');
    let front = document.getElementById('AFront');
    front.src = "/no-image.png";
    let back = document.getElementById('ABack');
    back.src = "/no-image.png";
    let img = document.getElementById('AImg');
    img.src = "/no-image.png";
    $('#AddGurantor').modal("toggle");
}
