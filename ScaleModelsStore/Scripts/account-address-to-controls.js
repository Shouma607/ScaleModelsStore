$(function () {
    $.post("/Manage/GetAddress", { "description": $('#Addresses').val() },
        function (data) {
            $('#PostalCode').val(data.PostalCode);
            $('#Country').val(data.Country);
            $('#City').val(data.City);
            $('#Address').text(data.Address);
        });

    $('#Addresses').change(function () {        
        $.post("/Manage/GetAddress", { "description": $(this).val() },
            function (data) {
                $('#PostalCode').val(data.PostalCode);
                $('#Country').val(data.Country);
                $('#City').val(data.City);
                $('#Address').text(data.Address);
            });
    });
});