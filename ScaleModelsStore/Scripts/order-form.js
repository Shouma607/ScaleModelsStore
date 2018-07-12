$(function () {    

    if ($('#DeliveryTypeId').val() === '1') {
        $('#DeliveryDependsBlock').hide();
    }
    $('#DeliveryTypeId').change(function () {
        if ($(this).val() === '1') {
            $('#DeliveryDependsBlock').hide();
        } else {
            $('#DeliveryDependsBlock').show();
        }
    });
});
