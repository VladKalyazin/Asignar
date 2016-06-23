$(document).ready(function () {
    $("#searchTool").click(function () {
        $("#searchText").fadeToggle(500);
    });
});

$(document).ready(function ($) {
    $(".clickable-row").click(function () {
        window.document.location = $(this).data("href");
    });
});