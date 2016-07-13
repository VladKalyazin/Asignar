$(document).ready(function ($) {
    $(".clickable-row").click(function () {
        window.document.location = $(this).data("href");
    });    
});


$("input[type='checkbox']").change(function () {
    if ($(this).is(":checked")) {
        $(this).parent().addClass("background_change");
    } else {
        $(this).parent().removeClass("background_change");
    }
});

$(function () {
    $("#add_project, #add_task, #add_user").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
   
    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-th-list close_popup\" aria-hidden=\"true\"> Create new</i>");
});


$(".open").click(function () {
    $("#add_task").css({ 'display': "inline" });
    $("#add_task").dialog("open");
    activatePlaceholders();
});

$("#opener").click(function () {
    $("#add_project").css({ 'display': "inline" });
    $("#add_project").dialog("open");
    activatePlaceholders();
});

$("#open_user").click(function () {
    $("#add_user").css({ 'display': "inline" });
    $("#add_user").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#add_user_to_project").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });

    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-users close_popup\" aria-hidden=\"true\"> Add user to project</i>");
});

$("#open_add_user").click(function () {
    $("#add_user_to_project").css({ 'display': "inline" });
    $("#add_user_to_project").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#add_filter").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });

    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-filter close_popup\" aria-hidden=\"true\"> Create new filter</i>");
});

$("#open_filter").click(function () {
    $("#add_filter").css({ 'display': "inline" });
    $("#add_filter").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#change_password").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-key close_popup\" aria-hidden=\"true\"> Change password</i>");
});

$("#open_change").click(function () {
    $("#change_password").css({ 'display': "inline" });
    $("#change_password").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#edit_user").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-pencil-square-o close_popup\" aria-hidden=\"true\"> Edit user data</i>");
});

$("#open_edit_user").click(function () {
    $("#edit_user").css({ 'display': "inline" });
    $("#edit_user").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#edit_project").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $(".ui-dialog-title").html("<i class=\"fa fa-list close_popup\" aria-hidden=\"true\"> Edit project</i>");
    $(".ui-dialog-titlebar-close").append("<i class=\"fa fa-times close_popup\" aria-hidden=\"true\"></i>");
});

$("#open_edit").click(function () {
    $("#edit_project").css({ 'display': "inline" });
    $("#edit_project").dialog("open");
    activatePlaceholders();
});


function activatePlaceholders() {
    $('[placeholder]').parents('form').submit(function () {
        $(this).find('[placeholder]').each(function () {
            var input = $(this);
            if (input.val() == input.attr('placeholder')) {
                input.val('');
            }
        })
    });

    $('[placeholder]').focus(function () {
        var input = $(this);
        if (input.val() == input.attr('placeholder')) {
            input.val('');
            input.removeClass('placeholder');
            input.css({ 'color': 'black', 'font-size': '100%' });
        }
    }).blur(function () {
        var input = $(this);
        if (input.val() == '' || input.val() == input.attr('placeholder')) {
            input.addClass('placeholder');
            input.val(input.attr('placeholder'));
            input.css({ 'color': 'rgba(0, 0, 0, 0.5)', 'font-size': '80%' });
        }
    }).blur();
}

