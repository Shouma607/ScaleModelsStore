$(function () {
    var sortedItems,
        getSortedAsc = function (selector, attrName) {
            return $(
                $(selector).toArray().sort(function (a, b) {
                    var aIntVal = parseInt(a.getAttribute(attrName)),
                        bIntVal = parseInt(b.getAttribute(attrName));
                    return aIntVal - bIntVal;
                })
            );
        },
        getSortedDesc = function (selector, attrName) {
            return $(
                $(selector).toArray().sort(function (a, b) {
                    var aIntVal = parseInt(a.getAttribute(attrName)),
                        bIntVal = parseInt(b.getAttribute(attrName));
                    return bIntVal - aIntVal;
                })
            );
        };
    $('#SortOptionsList').change(function () {
        if ($(this).val() === '0') {
            sortedItems = getSortedAsc('#sorting-start .item', 'sort-by-id').clone();
            $('#sorting-start').html(sortedItems);
        }
        if ($(this).val() === '1') {
            sortedItems = getSortedDesc('#sorting-start .item', 'sort-by-price').clone();
            $('#sorting-start').html(sortedItems);
        }
        if ($(this).val() === '2') {
            sortedItems = getSortedAsc('#sorting-start .item', 'sort-by-price').clone();
            $('#sorting-start').html(sortedItems);
        }
        if ($(this).val() === '3') {
            sortedItems = getSortedDesc('#sorting-start .item', 'sort-by-id').clone();
            $('#sorting-start').html(sortedItems);
        }
    });
});