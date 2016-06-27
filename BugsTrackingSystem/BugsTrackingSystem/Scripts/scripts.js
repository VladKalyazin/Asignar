$(document).ready(function ($) {
    $(".clickable-row").click(function () {
        window.document.location = $(this).data("href");
    });
});

$("#add_project")
    .dialog({
        modal: true,
        draggable: false,
        resizable: false,
        position: ['center', 'top'],
        width: 400,
        buttons: {
            "Add": function() {
                $(this).dialog("close");
            }
        }
});