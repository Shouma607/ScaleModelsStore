$(function () {
    var toCheck = parseInt($('#to-cart').attr("data-id"));
    if (toCheck > 0) {
        $('#to-cart').text('Add to Cart');
    } else {
        $('#to-cart').text('Out of Stock');
        $('#to-cart').prop('disabled', true);
        $('#to-cart').addClass("disabled");
    }
});