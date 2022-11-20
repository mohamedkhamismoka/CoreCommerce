$(document).ready(function () {
    $(".unAuth").click(function () {
        Swal.fire({
            icon: 'error',
            title: 'Sorry',
            text: 'You Must Login first!',

        })
    })

    $(".Auth").click(function () {

        var username = $(".username").val();
        $.ajax({
            url: '/Client/Client/Cart',
            method: 'Post',
            data: { username: username },
            success: function (response) {
                window.location.href = response.redirectToUrl
            }
        })
    })

    $(".add-Auth").click(function () {
        var id = $(this).parent().parent().find(".id").val();
        var username = $(".username").val();
        $.ajax({
            url: '/Client/Client/AddToCart',
            method: 'Post',
            data: { username: username, productid: id },
            success: function (response) {
                alert(response.message);
                if (response.message == "Product added Successfully to your cart") {

                  
                    $(".counter").html(response.cartcount)
                }
            }
        })

    })
})