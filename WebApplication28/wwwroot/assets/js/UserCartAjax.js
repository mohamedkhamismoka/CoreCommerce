$(document).ready(function () {

    function generatebill() {
        var unit = 0;
        $(".products  tr").each(function () {
            var ele = parseInt($(this).find("td:last-child .price").html()) * parseInt($(this).find(".quantity").val())



            unit += ele;
        })

        var total = (unit * 10 / 100);
        $(".bill tbody tr").first().find("td:nth-child(2)").html(unit.toString());
        $(".bill  tbody tr:nth-child(2)").find("td:nth-child(2)").html(unit * 10 / 100);
        $(".bill tbody tr:nth-child(3)").find("th:nth-child(2)").html(unit + total);

    }


    generatebill();

    var prev_quantity = $(".quantity").val();
    $(".quantity").change(function () {
        var quantity = $(this).val();
        var id = $(this).parent().parent().find(".id").html();
        var cart = $(".cart").val();
        $.ajax({
            url: "/Client/Client/CheckProduct",
            method: "Post",
            data: {
                quantity: quantity,
                productid: id,
                cartid: cart
            },
            success: function (response) {
                if (response.status == "fail") {
                    alert("Sorry,Not enough quantity in stock");
                    $(".quantity").val(prev_quantity)
                } else {
                    prev_quantity = $(".quantity").val();
                    generatebill();
                }

            }
        })

    })


    $(".Remove").click(function () {
        var productid = $(this).parent().parent().find(".id").html();


        var cart = $(".cart").val();

        $.ajax({
            url: "/Client/Client/RemoveFromCart",
            method: "Post",
            data: {
                productid: productid,
                cartid: cart
            },
            success: function (response) {
                if (response.status == "ok") {

                    $(this).parent().parent().remove();
                    generatebill();
                }
            }

        })

    })

    $(".SaveForLater").click(function () {
        var productid = $(this).parent().parent().find(".id").html();

        var ele = $(this).parent().parent();
        var cart = $(".cart").val();

        $.ajax({
            url: "/Client/Client/ChangeState",
            method: "Post",
            data: {
                productid: productid,
                cartid: cart
            },
            success: function (response) {
                if (response.status == "ok") {
                    $(".savings  ").append("<tr>" + ele + "</tr>");
                    $(".products ").remove($(this).parent().parent())

                    generatebill();
                }
            }

        })

    })

    $(".savings .remove").click(function () {
        var productid = $(this).parent().parent().find(".id").html();


        var cart = $(".cart").val();

        $.ajax({
            url: "/Client/Client/RemoveFromCart",
            method: "Post",
            data: {
                productid: productid,
                cartid: cart
            },
            success: function (response) {
                if (response.status == "ok") {

                    $(this).parent().parent().remove();

                }
            }

        })

    })

})