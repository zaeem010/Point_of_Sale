function Getproducts(code) {
    $.ajax({
        url: '/Pos/Sale/GetProductbyCatId',
        type: "GET",
        data: { "Id": code },
        success: function (data) {
            $(".cc").empty();
            var s = '';
            for (var i = 0; i < data.length; i++) {
                let imgpath = "/no-image.png";
                if (data[i].imagePath != null) {
                    imgpath = `/Uploads/${data[i].imagePath}`;
                }
                s += `<div class="col-xl-3">
                                    <div class="productCard">
                                        <a onclick="return add_tocart(${data[i].id})">
                                            <div class="productThumb">
                                                <img class="img-fluid" src="${imgpath}">
                                            </div>
                                            <div class="productContent text-center">
                                                ${data[i].name} <b>Sp:${data[i].salePrice}</b>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="btn-group">
                                        <button type="button" onclick="inner(id ,this.id), outer(value ,this.value),add_tocart1();" class="btn btn-primary" value="1" id="2">2</button>
                                        <button type="button" onclick="inner(id ,this.id), outer(value ,this.value),add_tocart1();" class="btn btn-primary" value="1" id="3">3</button>
                                        <button type="button" onclick="inner(id ,this.id), outer(value ,this.value),add_tocart1();" class="btn btn-primary" value="1" id="4">4</button>
                                        <button type="button" onclick="inner(id ,this.id), outer(value ,this.value),add_tocart1();" class="btn btn-primary" value="1" id="5">5</button>
                                    </div>
                                    <div class="btn-group">
                                        <input type="number" id="ManualVal" class="form-control" value="1" style="background:white;height:45px" autofocus />
                                        <button type="button" onclick="outer(value ,this.value),add_tocart2();" class="btn btn-primary" value="1"><i class="fa fa-check"></i></button>
                                    </div>
                                </div>`;
            }
            $('.cc').html(s);
        }
    });
    //$("#yes").prop("checked", true);
    //$('#PriceQty').val('1');
}