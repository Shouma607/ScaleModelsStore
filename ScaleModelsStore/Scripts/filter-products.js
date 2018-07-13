$(function () {
    $("#isFilterShown").click(function () {
        if ($(this).prop('checked') === true) {
            $(".filter-options").css("display", "block");
        } else {
            $(".filter-options").css("display", "none");
        }
    });
});