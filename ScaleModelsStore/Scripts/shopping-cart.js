$(function () {
    $(".CancelLink").click(function () {
        var recordToDelete = $(this).attr("data-id");
        if (recordToDelete !== '') {
            $.post("/ShoppingCart/RemoveRecord", { "id": recordToDelete },
                function (data) {
                    $('#row-1-' + data.DeleteId).fadeOut('slow');
                    $('#row-2-' + data.DeleteId).fadeOut('slow');
                    $('#cart-total').text(data.CartTotal);
                    $('#update-msg').text(data.Message);
                    $('#cart-status').text('Cart (' + data.CartQuantity + ')');
                });
        }
    });

    $(".RemoveUnitLink").click(function () {
        var recordToDelete = $(this).attr("data-id");
        var checkQuantity = $(this).attr("qntt");
        if (parseInt(checkQuantity) === 1) {
            $('#update-msg').text('Quantity must be at least 1');
            return;
        }
        if (recordToDelete !== '' && parseInt(checkQuantity) !== 1) {
            $.post("/ShoppingCart/RemoveUnit", { "id": recordToDelete },
                function (data) {
                    $('#item-qntt-' + data.ChangeId).text(data.ItemQuantity);
                    $('#cart-total').text(data.CartTotal);
                    $('#update-msg').text(data.Message);
                    $('#cart-status').text('Cart (' + data.CartQuantity + ')');
                    $("a[class='RemoveUnitLink'][data-id="+recordToDelete+"]").attr("qntt", parseInt(checkQuantity) - 1);
                    $("a[class='AddUnitLink'][data-id=" + recordToDelete + "]").attr("qntt", parseInt(checkQuantity) - 1);
                });
        }
    });
    $(".AddUnitLink").click(function () {
        var recordToAdd = $(this).attr("data-id");
        var checkQuantity = $(this).attr("qntt");
        var maxItemsQuantity = $(this).attr("max-qntt");
        if (parseInt(checkQuantity) === parseInt(maxItemsQuantity)) {
            $('#update-msg').text('You can not add more than ' + maxItemsQuantity + ' units of this item');
            return;
        }
        if (recordToAdd !== '' && parseInt(checkQuantity) !== parseInt(maxItemsQuantity)) {
            $.post("/ShoppingCart/AddUnit", { "id": recordToAdd },
                function (data) {
                    if (parseInt(checkQuantity) !== parseInt(data.ItemQuantity)) {
                        $('#item-qntt-' + data.ChangeId).text(data.ItemQuantity);
                        $('#cart-total').text(data.CartTotal);
                        $('#update-msg').text(data.Message);
                        $('#cart-status').text('Cart (' + data.CartQuantity + ')');
                        $("a[class='AddUnitLink'][data-id="+recordToAdd+"]").attr("qntt", parseInt(checkQuantity) + 1);
                        $("a[class='RemoveUnitLink'][data-id=" + recordToAdd + "]").attr("qntt", parseInt(checkQuantity) + 1);
                    } else {
                        $('#update-msg').text('Item is out of stock');
                        return;
                    }
                });
        }
    });
});