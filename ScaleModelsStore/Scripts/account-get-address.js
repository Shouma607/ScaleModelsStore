$(function () {  
    $('#Addresses').change(function () {        
        $.post("/Manage/GetAddress", { "description": $(this).val() },
            function (data) {
                $('#PostalCode').text(data.PostalCode);
                $('#Country').text(data.Country);
                $('#City').text(data.City);
                $('#Address').text(data.Address);
            });
    });
});