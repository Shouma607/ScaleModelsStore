$(function () {

    if ($("#isFilterShown").prop('checked') === true) {
        $(".filter-options").css("display", "block");
    } else {
        $(".filter-options").css("display", "none");
    }
    
    $("#isFilterShown").click(function () {
        if ($(this).prop('checked') === true) {
            $(".filter-options").css("display", "block");
        } else {
            $(".filter-options").css("display", "none");
        }
    });

    $("#reset").click(function () {
        $("#isFilterShown").prop('checked', false);
        $("#FilterByCategoryId").empty();
        $("#FilterByManufacturerId").empty();
        $("#FilterByScale").empty();
        $("#hiddenSearchResult").val("")
    });
});